Assumptions/Strategy
	1. The solution is composed of :
		1. Domain layer and Application layer
		2. Application services don't implement any interface (too much boilerplate for this POC)
		3. Unit Tests
	2. No database (as suggested) => Only Interfaces for repositories
		1. Interfaces followed a basic CQRS approach : queries and commands
		2. All interfaces and their methods are mocked (Unit Tests)
	3. Development towards DDD
		1. Auction and Vehicles has their own rules to keep their state
		2. Custom exceptions
		3. Each concrete vehicle is subclass
		4. Error Handling via Domain Exceptions => also helped to develop unit test asserts for exceptions cases (avoiding hard coded strings)
		5. Factory pattern introduced for vehicles creation
		6. No primitive obsession, through usage of vehicles specifications
	4. Basic Points
		1. Mindset for this development had 2 major targets: robust domain model and re-factoring; at same time unit testing was being implemented.
		2. Clean architecture with Solid principles in mind
		3. High Test coverage - more then 80%
		4. No comments were left behind => every piece of code is clear and indicate what is the purpose