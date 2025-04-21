# Alex_u199133_Scripting
 The scripting tutorial plus some modifications stated below:
1. First i finished part 2 also, to add the modifications that they proposed   
2. Added a Camera shake every 10 seconds, which indicates that the game is going faster from that point. So basically the speed goes up by 50%, so sheeps are faster every 10 seconds.
3. Added a Golden Sheep that counts double when hit, with a spawn rate of 5%.
4. Also an Enemy Red sheep that substracts one point when hit, with a spawn rate of 20%.
5. added a power up that activates each time a player hits 2 golden sheeps. Basically the fire rate goes up for 5 seconds. I intended to do a piercing shot that hit multiple sheep at once, but had some problems and decided to move from that idea. In fact, the variables for the power up are still called Penetrante..., so in case you look at the code i wanted to let you know.

I wanted to add more things but this past two weeks i didn't have much time.
And well, i added some background music and a sound when hitting a red sheep, but it's pretty basic so it didn't seem worth mentioning it in the list above.


Trying it again today (monday 21st) for doing the build i noticed that sometimes the spawn rate doesn't work as intended, i guess i did something wrong. I used random numbers and check whether a random number is smaller than te percentage of spawn of the entities of each different type, and if not just spawn a normal sheep. Maybe i did something wrong with the way it's calculated, or maybe it's simply a bug.
