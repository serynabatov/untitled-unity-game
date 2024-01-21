//Этот диалог предназначен для НПС головоломки с водой. С ним игрок встречается уже после того, как поговорил с основным НПС.
INCLUDE globals.ink
 //Эта переменная извне должна стать true в тот моент, когда игрок погвоорит с основным НПС.
{mainVarWaterFinished: ->main.finishedPuzzle|{mainVarWater: ->main.prePurpose|{certificate: ->main|->main.exit}}}

=== main ===
Наконец-то, это вы принц. Я ваш проводник для этого испытания. Вы должны продемонстрировать ваше умение находить путь из сложных ситуаций. #speaker:Wizard #portrait:wizardFace 
~ mainVarWater = true
->purpose

= purpose
Чтобы пройти испытание, вы должны направить русло воды. Источник воды в лесу заражён тьмой, нужно направить поток воды и восстановить течение.
->END

= prePurpose
Принц, я вижу вы ещё не прошли испытание. Вам напомнить о его деталях? #speaker:Wizard #portrait:wizardFace
+[Нужна подсказка]
->purpose
+[Нет, до свидания]
->finishedPuzzle
+[ПРОЙТИ ИСПЫТАНИЕ] #start:Riddle 1 #exit:0
->END

= exit
~ mainVarWater = true
Вы кто ?, я не вижу у вас право проходить у меня испытания для спасения принцессы. Может вам стоит получить этот сертификат у моего брата. Он похож на меня, только у него зелёная шляпа. #speaker:Water #portrait:wizardFace
->END

= finishedPuzzle
Так держать принц, вы восстановили русло воды #speaker:Water #portrait:wizardFace
->END