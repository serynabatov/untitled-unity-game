//Этот диалог предназначен для НПС головоломки с ящиками. С ним игрок встречается уже после того, как поговорил с основным НПС.
INCLUDE globals.ink
 //Эта переменная извне должна стать true в тот моент, когда игрок погвоорит с основным НПС.
{mainVarCrateFinished: ->main.finishedPuzzle|{mainVarCrate: ->main.prePurpose|{certificate: ->main|->main.exit}}}
=== main ===
Наконец-то, это вы принц. Я ваш проводник для этого испытания. Испытание проверит вашу внимательность. #speaker:Лемас #portrait:Violet Mage 
~ mainVarCrate = true
->purpose

= purpose
Чтобы пройти испытание, вы должны перетащить ящики, на то место в котором спрятана их тень.
->END

= prePurpose
Принц, я вижу вы ещё не прошли испытание. Вам напомнить о его деталях ? #layout:left #speaker:Лемас #portrait:Violet Mage
+[Нужна подсказка]
->purpose
+[Нет, до свидания] #exit:0
->END
+[ПРОЙТИ ИСПЫТАНИЕ] #start:Crate Puzzle #exit:0
->END 

= exit
Вы кто ?, я не вижу у вас право проходить у меня испытания. Может вам стоит получить сертификат у моего брата. Он похож на меня, только в зелёном плаще. #speaker:Crate #portrait:heroFace
~ mainVarCrate = true
->END

= finishedPuzzle
Поздравляю вас принц вы прошли испытание внимательности. ##speaker:Лемас #portrait:Violet Mage
+[ПРОЙТИ ИСПЫТАНИЕ]->END #start:Crate Puzzle #exit:0