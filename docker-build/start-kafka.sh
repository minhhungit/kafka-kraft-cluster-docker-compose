#!/bin/bash

/kafka/bin/kafka-storage.sh format -t 76BLQI7sT_ql1mBfKsOk9Q -c /kafka/config/kraft/server.properties --ignore-formatted

/kafka/bin/kafka-server-start.sh /kafka/config/kraft/server.properties