# Node.js app Docker file

FROM neteoc/neteoc-ui:latest

WORKDIR /opt/neteoc-server
RUN npm install -g mocha

COPY ./package.json /opt/neteoc-server
RUN npm install

COPY . /opt/neteoc-server

#RUN mocha

EXPOSE 3333

CMD ["npm", "start"]
