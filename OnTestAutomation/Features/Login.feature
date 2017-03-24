Feature: Login
	In order to access restricted site options
	As a registered ParaBank user
	I want to be able to log in

Scenario Outline: Login using valid credentials
	Given I have a registered user <firstname> with username <username> and password <password>
	And he is on the ParaBank home page
	When he logs in using his credentials
	Then he should land on the Accounts Overview page
	Examples:
	| firstname | username | password |
	| John      | john     | demo     |
	| Bob       | parasoft | demo     |
#	| Alex      | alex     | demo     |

Scenario:  Login using incorrect password
	Given I have a registered user <firstname> with username <username> and password <password>
	And he is on the ParaBank home page
	When he logs in using an invalid password
	Then he should see an error message stating that the login request was denied