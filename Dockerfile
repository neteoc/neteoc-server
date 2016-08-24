# Node.js app Docker file

FROM ubuntu:16.04


RUN apt-get update

#RUN curl -sL -o nodeinstall.sh https://deb.nodesource.com/setup_6.x && chmod +x nodeintall.sh && ./nodeinstall.sh



RUN mkdir -p /opt/flare
WORKDIR /opt/flare

COPY ./scripts/nodesetup.sh /opt/flare
RUN ./nodesetup.sh && apt-get install -y nodejs

COPY ./package.json /opt/flare
RUN npm install

COPY . /opt/flare

EXPOSE 3333




CMD ["npm", "start"]
