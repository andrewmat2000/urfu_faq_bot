import asyncio
import uvicorn

from os import environ

from queue import Queue
from fastapi import FastAPI

from deeppavlov import train_model
from deeppavlov.utils.telegram import interact_model_by_telegram
from deeppavlov.utils.alice.request_parameters import data_body
from deeppavlov.utils.alice.server import AliceBot
from deeppavlov.utils.server import redirect_root_to_docs


model_config="./configs/tfidf_logreg_autofaq.json"

train_model(model_config, download=True)

app = FastAPI()

host = "0.0.0.0"
port = 80
endpoint = "/alice"

input_q = Queue()
output_q = Queue()

bot = AliceBot(model_config, input_q, output_q)
# bot._model = faq
bot.start()

redirect_root_to_docs(app, 'answer', endpoint, 'post')

@app.post(endpoint, summary='A model endpoint', response_description='A model response')
async def answer(data: dict = data_body) -> dict:
    loop = asyncio.get_event_loop()
    bot.input_queue.put(data)
    response: dict = await loop.run_in_executor(None, bot.output_queue.get)
    return response

async def run_tg():
    interact_model_by_telegram(model_config, token=environ["TELEGTAM_TOKEN"])

run_tg()

uvicorn.run(app=app, host=host, port=port)

bot.join()