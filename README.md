# Avoid common pitfalls when using Async, Await and CancellationToken
Here we go over a few common usage scenarios when using async, await and cancellation tokens:

- Parallel processing with tasks to increase appilcation throughput
- Exception handling with using Task.WhenAll
- Creating CancellationTokenSource and passing the token to async methods
- Async void scenario
  - Usage scenarios (Event Handlers)
  - Exception handling
  - Why is it a BAD IDEA
