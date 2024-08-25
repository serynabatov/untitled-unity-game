//Этот диалог предназначен для БОССА-НПС. С ним игрок встречается уже после того, как поговорил с основным НПС.
INCLUDE globals.ink
{mainVarBossFinished: ->main.finishedBoss|{cantalktoBoss: { mainVarBoss: ->main.prePurpose|->main}}}


=== main ===
We meet again. Finally, you've passed all the tests, the last one is left. Only the worthy can possess the flower, so don't think you're worthy after passing all the tests.  #speaker:Ados #portrait:Green Mage
But don't worry, this is the easiest test, all you have to do is answer the question.
~ mainVarBoss = true
->prePurpose

= prePurpose
Do you promise that you will fight to the end, even when you lose all hope and even when you forget your goal?
+[Yes, I promise] 
->finishedBoss
+[No, I'm not] #start:ExampleRiddle #exit:0
->END
+[I can't say] #start:ExampleRiddle #exit:0
->END

= finishedBoss
You've proven you can be trusted, hold the flask and fill it with water from the holy spring.  #speaker:Ados #portrait:Green Mage
One more thing. Thank you!
~ mainVarBossFinished = true
->END