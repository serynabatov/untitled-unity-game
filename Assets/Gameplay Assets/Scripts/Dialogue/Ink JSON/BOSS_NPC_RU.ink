//Этот диалог предназначен для БОССА-НПС. С ним игрок встречается уже после того, как поговорил с основным НПС.
INCLUDE globals.ink
{mainVarBossFinished: ->main.finishedBoss|{cantalktoBoss: { mainVarBoss: ->main.prePurpose|{certificate: ->main|->main.exit}}| ->main.exit}}


=== main ===
Я ждал тебя. Наконец ты прошёл все испытания, осталось последнее. Цветок не только может спасти принцессу, но и дать власть тому, кто его использует. Поэтому задам тебе вопрос #speaker:Вадос #portrait:Green Mage
~ mainVarBoss = true
->prePurpose

= prePurpose
Принц, обещаешь ли ты что удержишься от соблазна и спасёшь принцессу, а не присвоишь цветок себе?
+[Да, обещаю]
->END
+[Нет, не обещаю] #start:ExampleRiddle #exit:0
->END
+[Не могу сказать] #start:ExampleRiddle #exit:0
->END

= exit 
Рыцарь тебе рано приходить ко мне. Сначала пройди испытания, которые для тебя подготовили мои братья #speaker:Вадос #portrait:Green Mage
->END

= finishedBoss
Ты доказал, что тебе можно доверять, держи флягу и наполни её водой из святого источника.  #speaker:Вадос #portrait:Green Mage
И ещё одно.
Спасибо тебе
->END