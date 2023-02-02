# Redis implemented

[Redis](https://redis.io) (Remote Dictionary Server) is an open-source, in-memory data structure store, used as a database, cache, and message broker. It supports a variety of data structures such as strings, hashes, lists, sets, sorted sets with range queries, and more.

### Some of Redis features

* In-memory data store: Redis stores all its data in memory, which provides low latency and high performance.
* Pub/Sub messaging: Redis supports publish/subscribe messaging, making it a powerful tool for real-time communication between applications.
* Key-value store: Redis provides a simple, fast, and flexible key-value store, making it ideal for use cases where data needs to be stored and retrieved quickly.
* Stream: Streams is a powerful feature in Redis that allows for the creation and management of ordered, fault-tolerant, and scalable event streams.

### Redis also supports

* Lua scripting: Redis supports Lua scripting, which enables server-side processing and the execution of custom commands.
* High availability and scalability: Redis supports a variety of methods for ensuring high availability, including Redis Sentinel and Redis Cluster, which allow Redis to be deployed as a highly available and scalable cluster.
* Persistence: Redis supports persistence options that allow data to be saved to disk and loaded back into memory, ensuring data durability.

> ### This repository is aims to implement an easy to use example for all of Redis features

## How to contribute

1. Clone the repository: Clone the repository to your local development environment.

2. Create a new branch: Create a new branch for your changes, this helps keep your changes separate from the main codebase.

3. Make changes: Make changes to the code, fix bugs, or add new features.

4. Commit changes: Commit your changes to your local repository, pls use [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/#commit-message-with-scope).

5. Push changes: Push your changes to your forked repository on GitHub.

6. Create a pull request: Create a pull request to submit your changes to the main repository. Be sure to provide a clear and concise description of your changes and why they are needed.

7. Respond to feedback: If the maintainers of the repository have any feedback or questions about your changes, be sure to respond in a timely manner.

## Redis Streams with .NET as Producer and Consumer

How to use Redis Streams with .NET [Official documentation](https://developer.redis.com/develop/dotnet/streams/stream-basics/)

## Prerequisites

* .NET SDK, installation can be found [here](https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net70)
* Docker, installation can be found [here](https://www.docker.com/)

## How to

1. Check Prerequisites
2. Clone the repository
3. Execute docker command, [docker compose up](https://docs.docker.com/engine/reference/commandline/compose_up) `docker compose up` in root of project, using your command prompt. This will start the local redis server using.
4. Navigate to "net-redis\Redis.Stream.Consumer", using your command prompt. [Run](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build) `dotnet build`. This will start the consumer (worker).
5. In soulution run Test `ShouldEmitEntryToRedisStream`

#### .NET Producer

Implemented example can be found [here](https://github.com/barbarossa426/net-redis/tree/main/Redis.Stream.Producer)

#### .NET Consumer

Implemented example can be found [here](https://github.com/barbarossa426/net-redis/tree/main/Redis.Stream.Consumer)
