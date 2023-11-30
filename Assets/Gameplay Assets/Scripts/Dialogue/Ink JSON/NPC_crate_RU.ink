//Этот диалог предназначен для НПС головоломки с ящиками. С ним игрок встречается уже после того, как поговорил с основным НПС.
INCLUDE globals.ink
 //Эта переменная извне должна стать true в тот моент, когда игрок погвоорит с основным НПС.
{mainVarCrate: ->main.prePurpose|{certificate: ->main|->main.exit}}

=== main ===
Наконец-то, это вы принц. Я ваш проводник для этого испытания терпения. Только пройдя его, вы сможете получить от меня заветный ключ #layout:left #speaker:Crate #portrait:heroFace
~ mainVarCrate = true
->purpose

= purpose
Чтобы пройти испытание, вы должны передвигать ящики. #layout:left #speaker:Crate #portrait:heroFace
->END

= prePurpose
Принц, я вижу вы ещё не прошли испытание. Вам напомнить о его деталях ? #layout:left #speaker:Crate #portrait:heroFace
+[Нужна подсказка]
->purpose
+[Нет, до свидания]
->END
+[ПРОЙТИ ИСПЫТАНИЕ]->END #start:Crate Puzzle 

= exit
Вы кто ?, я не вижу у вас право проходить у меня испытания для спасения принцессы. Может вам стоит получить этот сертификат у моего брата. Он похож на меня, только у него белая шляпа. #layout:left #speaker:Crate #portrait:heroFace
~ mainVarCrate = true
->END