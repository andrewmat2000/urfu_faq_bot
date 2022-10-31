# FROM python:3.7-alpine

# # RUN adduser -D python

# RUN apk add --update python3 py3-pip build-base

# # USER python

# WORKDIR /opt/python_service
# COPY ./app.py .
# COPY ./start.sh .
# COPY ./data .
# COPY ./configs .

# # COPY ./ ~/python_service

# #RUN pip install Flask

# RUN chmod +x ./start.sh
# RUN python3 -m venv venv
# RUN chmod +x venv/bin/pip
# RUN source venv/bin/activate && python3 -m pip install --upgrade pip && pip install deeppavlov && pip install flask


# CMD './start.sh'

FROM deeppavlov/base-cpu:0.17.6

# COPY ./app.py .
# COPY ./start.sh .
WORKDIR /base/
COPY ./data ./data
COPY ./configs ./configs
COPY ./app.py .
COPY ./start.sh .
RUN chmod +x ./start.sh
RUN /bin/bash -c "source venv/bin/activate && pip3 install flask"

CMD './start.sh'

# WORKDIR /base/DeepPavlov
# ENTRYPOINT [ "python3" ]
# CMD [ '_main__.py' 'CONFIG=/configs/tfidf_logreg_autofaq.json' '-p' '80:80' ]