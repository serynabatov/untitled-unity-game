INCLUDE globals.ink
{ 
- mainVarCrateFinished and mainVarWaterFinished:->main.FinalCondition
- mainVarWaterFinished:->main.WaterCondition
- mainVarCrateFinished:->main.CrateCondition
- else:->main
}

=== main ===
Darkness has broken through the barrier, reached the river, and blocked its flow. Help dispel the darkness and open a path for the water. #speaker:Ailin #portrait:Princess
There is a wizard standing by the river now; with his help, you can dispel the darkness. Just ignore his mannerisms: old wizards have their own quirks. 
Who they really are is a mystery to me, but they are trustworthy. 
I think they are the embodiments of this magical place, as if the flower itself created them to test people, to penetrate their souls. The unworthy will be cast out of this place.
->END

= WaterCondition
Congratulations, you've succeeded in making a path for the water. Now we just have to pass the second brother's test. He should have an easier time. #speaker:Эйлин #portrait:Princess
->END

= CrateCondition
You've managed to put the crates in the right place, what's left is a path for the water. Without it, you can't activate the flower's power. #speaker:Эйлин #portrait:Princess
->END

= FinalCondition
You have succeeded in completing both major challenges. Hurry up and keep going, it's just a little bit longer. #speaker:Эйлин #portrait:Princess
->END
