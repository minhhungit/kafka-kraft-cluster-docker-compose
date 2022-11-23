# https://hub.docker.com/_/openjdk/tags?page=1&name=jre-slim-buster
FROM openjdk:11.0.16-jre-slim-buster

RUN apt-get update && \
    apt-get install -y curl
         
ENV KAFKA_VERSION 3.3.1
ENV SCALA_VERSION 2.13 

RUN mkdir /tmp-download && \
	mkdir /tmp-download/kafka && \
    curl "https://archive.apache.org/dist/kafka/${KAFKA_VERSION}/kafka_${SCALA_VERSION}-${KAFKA_VERSION}.tgz" \
    -o /tmp-download/kafka/kafka.tgz && \
    mkdir /kafka && cd /kafka && \
    tar -xvzf /tmp-download/kafka/kafka.tgz --strip 1 && \
	rm -fr /tmp-download

RUN mkdir /kafka-data && \
	mkdir /kafka-data/kraft-combined-logs

COPY start-kafka.sh /usr/bin

RUN chmod +x /usr/bin/start-kafka.sh

CMD ["start-kafka.sh"]