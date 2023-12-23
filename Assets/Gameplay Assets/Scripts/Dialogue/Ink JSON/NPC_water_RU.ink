//Этот диалог предназначен для НПС головоломки с водой. С ним игрок встречается уже после того, как поговорил с основным НПС.
INCLUDE globals.ink
 //Эта переменная извне должна стать true в тот моент, когда игрок погвоорит с основным НПС.
{mainVarWaterFinished: ->main.finishedPuzzle|{mainVarWater: ->main.prePurpose|{certificate: ->main|->main.exit}}}

=== main ===
Наконец-то, это вы принц. Я ваш проводник для этого испытания внимательности и усидчивости. Только пройдя его, вы сможете получить от меня заветный ключ #layout:right #speaker:Wizard #portrait:wizardFace 
~ mainVarWater = true
->purpose

= purpose
Чтобы пройти испытание, вы должны направить русло воды. Источник воды в лесу обмелел, нужно направить поток воды и восстановить течение.
Нажимайте на кнопки воды, чтобы повернуть их, таким образом вы должны в правильную сторону повернуть все ячейки и направить русло воды до конечной точки.
->END

= prePurpose
Принц, я вижу вы ещё не прошли испытание. Вам напомнить о его деталях ? #layout:right #speaker:Wizard #portrait:wizardFace 
+[Нужна подсказка]
->purpose
+[Нет, до свидания]
->finishedPuzzle
+[ПРОЙТИ ИСПЫТАНИЕ]
->startPuzzle

= exit
~ mainVarWater = true
Вы кто ?, я не вижу у вас право проходить у меня испытания для спасения принцессы. Может вам стоит получить этот сертификат у моего брата. Он похож на меня, только у него белая шляпа. #layout:right #speaker:Water #portrait:wizardFace
->END

= finishedPuzzle
Я величайший и прошёл загадку с водой #layout:right #speaker:Water #portrait:wizardFace
->END

= startPuzzle
Ну чё погнали проходить головоломку с водой
#start:Riddle 1 #exit:0,2
->END 