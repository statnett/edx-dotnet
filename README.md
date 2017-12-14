# EDX extension for AmqpNetLite

This is a small extension over AmqpNetLite for simplifying communication with an EDX-Toolbox. The most important features are:

1. Encapsulating AMQP Application Properties that regularly would be [string-based](EdxLib/Constants.cs) 
1. Unwrapping binary message content as a string when receiving messages

The solution also provides examples on how to connect with EDX, as well as sending and receiving messages.

### Contents
There are four projects in this solution: 
1. [EdxLib](EdxLib), the library itself
1. [EdxLibTests](EdxLibTests), unit tests for the library-functionality
1. [SimpleSender](SimpleSender), a basic console application for sending data
1. [SimpleReceiver](SimpleReceiver), a basic console application for receiving data

Additionaly there is help on setting up a [stub environment](SETUP.md) for local development/testing

https://github.com/Azure/amqpnetlite/tree/master/test/Test.Amqp.Net

The samples on these pages are synchronous, amqpnetlite is however encouraging use of async methods where applicable:

http://azure.github.io/amqpnetlite/articles/building_application.html

For more examples, see the AmqpLiteNet home, which contains more complex examples of sending/receiving messages. These examples including transaction handling as well as callback-based communication: 

https://github.com/Azure/amqpnetlite/blob/master/Examples
