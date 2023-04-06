Feature: AutoMower


Scenario: exemple Mower 1
	Given a lawn with a top right corner at X equal to 5 and Y equal to 5
	And a mower at position x equal to 1 and y equal to 2  and orientation equal to N
	And a moveset equal to LFLFLFLFF
	When treatment is launched
	Then the mower last position is X equal to 1 and Y equal to 3 and orientation is equal to N


Scenario: exemple Mower 2
	Given a lawn with a top right corner at X equal to 5 and Y equal to 5
	And a mower at position x equal to 3 and y equal to 3  and orientation equal to E
	And a moveset equal to FFRFFRFRRF
	When treatment is launched
	Then the mower last position is X equal to 5 and Y equal to 1 and orientation is equal to E


Scenario: Example of a lawnmower exceeding the boundaries
	Given a lawn with a top right corner at X equal to 4 and Y equal to 4
	And a mower at position x equal to 0 and y equal to 0  and orientation equal to N
	And a moveset equal to FFFFFFFFFFFFFF
	When treatment is launched
	Then the mower last position is X equal to 0 and Y equal to 4 and orientation is equal to N