from os import environ

from deeppavlov import train_model
from deeppavlov.utils.telegram import interact_model_by_telegram

model_config="./configs/tfidf_logreg_autofaq.json"

train_model(model_config, download=True)

interact_model_by_telegram(model_config, token=environ["TELEGRAM_TOKEN"])