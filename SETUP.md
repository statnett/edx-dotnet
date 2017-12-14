# Stub environment for the examples

### Rationale
During development or initial testing you might not have an EDX-environment available. This doucment describes one way to set up a stub enviroment for testing connectivity, sending and receiving messages.

RabbitMQ is used as an AMQP broker. If you have RabbitMQ up and running already, you can use that instance in stead of following this document, but please read the warnings below.

### Warning!
This setup does not run EDX-validation, and will accept some messages that EDX will reject. 

Out of the box RabbitMQ uses the non-compatible AMQP 0.9.1 only. If you are setting up RabbitMQ in any other way, be sure to enable AMQP 1.0-support:

```
rabbitmq-plugins enable rabbitmq_amqp1_0
```

## Setup
### Prerequisites
Before you get started you do need [Docker](https://www.docker.com) installed, as this setup uses an image running [RabbitMQ](https://www.rabbitmq.com/) with [AMQP 1.0 Web Interface plugins](https://www.rabbitmq.com/plugins.html) enabled. 

The image we will be using can be found here:  
* Source: https://github.com/it-herz/docker-rabbitmq 
* Image: https://hub.docker.com/r/itherz/rabbitmq2/

### Start the image
Start the RabbitMQ container from the commandline:

```
docker run -d --name edxstub -p 5672:5672 -p 15672:15672 itherz/rabbitmq2
```
This container exposes ports for AMQP (5672) and the Web Interface (8161) and is named _edxstub_

### Set up the solution

Get docker-machine ip from the command line:
```
docker ip
```

This ip must be used in the App.config files of the given example you want to run. 

Update the field _EdxUrl_ in App.config with the docker machine ip, for instance:
```
<add key="EdxUrl" value="amqp://192.168.99.100:5672"/>
```

### Running the examples
The RabbitMQ-broker should now be able to accept messages. If you run the _SimpleSender_ example once, given a successful run, queues will be created.


To look at the queues, go to the management web interface, which is on the docker-machine under port 15672. 
Default username and password is _guest_. Example url will be something like: 

```
http://192.168.99.100:15672/
```

Hopefully you will see the number of messages succesfully sent in the _edx.endpoint.outbox_ queue.



###  Moving messages from outbox to inbox
Unless you feel like hand-crafting the reply messages, a good way to receive test messages is to return the ones you have sent. This can be done by enabling _rabbitmq_shovel_. 

__Note! Use of _rabbitmq_shovel_ has to be performed *after* the first attempt at reading a message from the given queue, or else RabbitMQ will create a persistent queue that is not supported directly by the AMQP 1.0-library.__

First, attach to the running docker instance by writing: 

```
docker exec -it edxstub /bin/bash
```

Then turn on the shovel-plugin:
```
rabbitmq-plugins enable rabbitmq_shovel rabbitmq_shovel_management
```

In the Web Interface, open the edx.endpoint.outbox queue, for instance: 
```
http://192.168.99.100:15672/#/queues/%2F/edx.endpoint.outbox
```

Under the "Move" section enter the destination queue, presumably _edx.endpoint.inbox_, and press the move-button.
You should now be able to receive the same messages you have sent.
