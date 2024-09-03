//Этот диалог предназначен для НПС головоломки с водой. С ним игрок встречается уже после того, как поговорил с основным НПС.
INCLUDE globals.ink
 //Эта переменная извне должна стать true в тот моент, когда игрок погвоорит с основным НПС.
{mainVarWaterFinished: ->main.finishedPuzzle|{mainVarWater: ->main.prePurpose|->main}}

=== main ===
You came. My brother told me about you. I am your guide for this trial. My name is <color=\#F8FF30><b>Gory</b></color>, emphasis on the first syllable. #speaker:Gory #portrait:Orange Mage
Remember your path-breaking skills, and if you don't have them, now acquire them.
~ mainVarWater = true
->purpose

= purpose
To pass the test, you must correctly direct the riverbed. In some places, the river is contaminated with darkness, but you can overcome it; the main thing is to be careful. I trust your instincts, so you will figure out the rest of the details on your own. 
This burden has fallen to you, and if you fail, no one will. 
+[I'll come back later.] #exit:0
->END
+[<b>PASS THE TEST</b>] #start:Riddle 1 #exit:0
->END

= prePurpose
Prince, I see you haven't passed the test yet. Shall I remind you of the details? #speaker:Gory #portrait:Orange Mage
+[Need a hint]
->purpose
+[No, goodbye] #exit:0
->END
+[<b>PASS THE TEST</b>] #start:Riddle 1 #exit:0
->END

= finishedPuzzle
Way to go, Prince! You <color=\#F8FF30>restored the riverbed</color>. I was sure you could do it. I'm proud of you. #speaker:Gory #portrait:Orange Mage
->END