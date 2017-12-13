For more complex examples, see AmqpLiteNet on github:
https://github.com/Azure/amqpnetlite/tree/master/test/Test.Amqp.Net

Contains send/receive examples, as well as transaction handling.

Use of async send/receive methods is encouraged:
http://azure.github.io/amqpnetlite/articles/building_application.html


WaitTime = 5; // TODO: Config
"ecp.endpoint.inbox"; // TODO: Config
"ecp.endpoint.outbox"; // TODO: Config
"ecp.endpoint.outbox.reply"; // TODO: Config


Receive vs. Attach. vs. Credit vs. Callback

https://github.com/Azure/amqpnetlite/blob/master/Examples