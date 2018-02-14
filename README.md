# EDX extension for AmqpNetLite

This is a small extension over AmqpNetLite for simplifying communication with an EDX-Toolbox. The most important features are:

1. Encapsulating AMQP Application Properties that regularly would be [string-based](EdxLib/Constants.cs) 
1. Unwrapping binary message content as a string when receiving messages

The solution also provides examples on how to connect with EDX, as well as sending and receiving messages.

### Contents
There are multiple projects in this solution: 
1. EdxLib, the library itself
1. EdxLibTests, unit tests for the library-functionality
1. SimpleSender, a basic console application for sending data through the outbox queue
1. SimpleReceiver, a basic console application for receiving data from the inbox queue
1. SimpleReplyReceiver, a basic console application for receiving status messages from the outbox reqply queue

Additionaly there is help on setting up a [stub environment](SETUP.md) for local development/testing with RabbitMQ

https://github.com/Azure/amqpnetlite/tree/master/test/Test.Amqp.Net

The samples given on this site are very simple, for better guidance on building AMQP-based applications please refer to:

http://azure.github.io/amqpnetlite/articles/building_application.html

There are also more complex examples at the AmqpLiteNet home, including transaction handling as well as callback-based communication: 

https://github.com/Azure/amqpnetlite/blob/master/Examples
