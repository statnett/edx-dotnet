# EDX extension for AmqpNetLite

This is a small extension over AmqpNetLite for simplifying communication with an EDX-Toolbox. The most important topics are:

1. Providing examples on how to connect with EDX.
1. Encapsulating AMQP Application Properties that regularly would be [string-based](EdxLib/Constants.cs) 
1. Unwrapping binary message content as a string when receiving messages

### Contents
There are four projects in this solution: 
1. [EdxLib](EdxLib), the library itself
1. [EdxLibTests](EdxLibTests), unit tests for the library
1. [SimpleSender](SimpleSender), a simple example for sending data 
1. [SimpleReceiver](SimpleReceiver), a simple example for receiving data

Additionaly there is help on setting up a [stub environment](SETUP.md) for local development/testing

For more complex examples, see the AmqpLiteNet home, which contains more complex send/receive examples. One example of transactional communication is also given:
https://github.com/Azure/amqpnetlite/tree/master/test/Test.Amqp.Net

The samples on these pages are synchronous, amqpnetlite is however encouraging use of async methods where applicable:
http://azure.github.io/amqpnetlite/articles/building_application.html

There are also callback-based communication examples available: 
https://github.com/Azure/amqpnetlite/blob/master/Examples
