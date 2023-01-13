import asyncio
import uvicorn

from queue import Queue
from fastapi import FastAPI

from deeppavlov import train_model
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

bot.start()

redirect_root_to_docs(app, 'answer', endpoint, 'post')

@app.post(endpoint, summary='A model endpoint', response_description='A model response')
async def answer(data: dict = data_body) -> dict:
    loop = asyncio.get_event_loop()
    bot.input_queue.put(data)
    response: dict = await loop.run_in_executor(None, bot.output_queue.get)
    if response['response']["text"] == 'Welcome to DeepPavlov inference bot!':
        response['response']['text'] = "Здравствуйте! Я чат-бот :). Можете задать любой вопрос по магистратуре УрФу, и я постараюсь на него ответить."
    return response

uvicorn.run(app=app, host=host, port=port)

bot.join()