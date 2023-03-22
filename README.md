<h1 align="center">
  <br>
  <a href="https://christian-schou.dk"><img src="https://github.com/Tech-With-Christian/Dotnet-Demo-CRUD-Api/blob/main/assets/img/cs-logo-polygon.png" alt="Christian Schou Logo" width="200"></a>
  <br>
  MassTransit .NET Demo
  <br>
</h1>

<h4 align="center">This is a demonstration of how easy it is to implement MassTransit in a simple .NET CRUD API. You can read the <a href="https://blog.christian-schou.dk/how-to-use-masstransit-with-rabbitmq" target="_blank">full tutorial here</a>. It contains a simple implementation of MassTransit with communication to RabbitMQ on a CRUD service for products, that can easily be extended. The service will create new products in the database using a publisher and consumer.</h4>


<p align="center">
<a href="https://blog.christian-schou.dk/how-to-use-masstransit-with-rabbitmq"><img src="https://github.com/Tech-With-Christian/Dotnet-CRUD-Api/blob/main/assets/img/github-masstransit-cover-image.png" alt="Featured Image .NET MassTransit CRUD API" width="90%"></a>
</p>

<p align="center">
  <a href="#key-features">Key Features</a> •
  <a href="#how-to-use">How To Use</a> •
  <a href="#download">Download</a> •
  <a href="#credits">Credits</a> •
  <a href="#license">License</a>
</p>


## Key Features

* MassTransit for easily building a reliable distributed application
  - Choose your own connector: RabbitMq, Azure Service Bus, Amazon SQS or In-Memory.
* Simple CRUD implementation
  - Super simple CRUD service implementation that can easily be extended.
* Error Handler Middleware
  - Global Error Handler Middleware taking care of exceptions in the applicaiton, providing the client with a consistent response every time. This can easily be extended with custom exceptions.
* Well documented Code (inline + wiki)
* Automatic mapping
  - Automatic Mapping of DTOs to/from Domain Models using <a href="https://automapper.org/">AutoMapper</a>.
* Database Integration using Entity Framework Core.
* MediatR (CQRS)
  - Implementation of CQRS using MediatR. All controllers will consume the mediator to handle commands and queries.

## How To Use

To clone and run this simple .NET Web API, you'll need [Git](https://git-scm.com) and [.NET SDK](https://dotnet.microsoft.com/en-us/download). From your command line:

```bash
# Clone this repository
$ git clone https://github.com/Tech-With-Christian/MassTransitDemo.git

# Go into the repository
$ cd masstransitdemo

# Restore dependencies
$ dotnet restore

# Run the app
$ dotnet watch
```

## Download

You can [download](https://github.com/Tech-With-Christian/MassTransitDemo/archive/refs/heads/main.zip) the latest main version of this MassTransit .NET CRUD API Demo for further development on Windows, macOS and Linux.

## Credits

This software uses the following open source packages:

- [RabbitMQ](https://www.rabbitmq.com/)
- [MassTransit](https://masstransit.io/)
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- [.NET Core](https://github.com/dotnet/core)
- [MediatR](https://github.com/jbogard/MediatR)
- [Swagger](https://github.com/swagger-api)

## License

MIT

---

> [christian-schou.dk](https://christian-schou.dk) &nbsp;&middot;&nbsp;
> GitHub [@Christian-Schou](https://github.com/Christian-Schou) &nbsp;&middot;&nbsp;
> Blog [blog.christian-schou.dk](https://blog.christian-schou.dk)
