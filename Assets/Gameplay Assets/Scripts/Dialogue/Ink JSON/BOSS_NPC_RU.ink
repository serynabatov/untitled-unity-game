//Этот диалог предназначен для БОССА-НПС. С ним игрок встречается уже после того, как поговорил с основным НПС.
INCLUDE globals.ink
{mainVarBossFinished: ->main.finishedBoss|{cantalktoBoss: { mainVarBoss: ->main.prePurpose|->main}}}


=== main ===
Снова встретились. Наконец, ты прошёл все испытания, осталось последнее. Цветком может владеть лишь достойный, не думай, что, пройдя все испытания, ты стал достоен.  #speaker:Адос #portrait:Green Mage
Но не беспокойся, это самое простое испытание, тебе нужно лишь ответить на вопрос.
~ mainVarBoss = true
->prePurpose

= prePurpose
Обещаешь ли ты, что будешь бороться до конца, даже когда потеряешь всякую надежду и даже когда забудешь свою цель?
+[Да, обещаю] 
->finishedBoss
+[Нет, не обещаю] #start:ExampleRiddle #exit:0,2
->END
+[Не могу сказать] #start:ExampleRiddle #exit:0,2
->END

= finishedBoss
Ты доказал, что тебе можно доверять, держи флягу и наполни её водой из святого источника.  #speaker:Адос #portrait:Green Mage
И ещё одно. Спасибо тебе!
~ mainVarBossFinished = true
->END