# ** Fibonacci API**

This is a WebAPI created by Renan Lucena attending the specification below:

Implement an API capable of generating and returning a subsequence from a sequence of Fibonacci
numbers. The API should have a controller with an endpoint accepting the following parameters:

REQUERIMENTS
1. The index of the first number in Fibonacci sequence that starts subsequence.
2. The index of the last number in Fibonacci sequence that ends subsequence.
3. A Boolean, which indicates whether it can use cache or not.
4. A time in milliseconds for how long it can run. If generating the first number in subsequence
takes longer than that time, the program should return error. Otherwise as many numbers as
were generated with extra information indicating the timeout occurred.
5. A maximum amount of memory the program can use. If, during the execution of the request
this amount is reached, the execution aborts. The program should return as many generated
numbers similarly to the way it does in case of timeout reached.

The return from the endpoint should be a JSON containing the subsequence from the sequence of
Fibonacci numbers that is matching the input indexes.

The controller that accepts requests could use async pattern. It could schedule the work on two
background threads and wait for results asynchronously. The generation of Fibonacci numbers could
happen on at least two background threads, where the next number in sequence could be generated
on a different thread.

Please bear in mind, there could be many requests landing simultaneously and those could use the
same background threads that are executing already.

There could be a cache for numbers, so that subsequent requests can rely on it in order to speed up
the Fibonacci numbers generation.

The cache could be invalidated after a time period, where the period is defined in configuration.



//END OF THE REQUERIMENTS


On my solution, you will find Clean Code (DRY, Classes, Variables and Methods name), Principles, SOLID Principles, Designer Patterns (Builder, Singleton), DDD organization, async programming, recursive.


For the tests, I created a XUnit project with some tests to check Exceptions and results using the AAA pattern.





## **Documentation**
![](https://blogs.mulesoft.com/wp-content/uploads/postman-anypoint.png)

**Documentation: ** The postman documentation with the correct body can be found [HERE](https://documenter.getpostman.com/view/3658752/TVK5d1qs)

**Tests Case: ** Also can be checked the test cases [HERE](https://documenter.getpostman.com/view/3658752/TVK5cLt3)


![](https://miro.medium.com/max/690/1*aKVg84SP5oPV9fwOnbl6yQ.png)

The Swagger Documentation is started when the API runs. I changed the default route to display http://localhost:49640/swagger/index.html   


