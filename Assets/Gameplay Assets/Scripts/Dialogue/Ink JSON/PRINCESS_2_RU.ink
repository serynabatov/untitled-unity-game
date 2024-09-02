INCLUDE globals.ink
{ 
- mainVarCrateFinished and mainVarWaterFinished:->main.FinalCondition
- mainVarWaterFinished:->main.WaterCondition
- mainVarCrateFinished:->main.CrateCondition
- else:->main
}

=== main ===
Тьма пробила брешь в барьере, добралась до реки и заблокировала её течение. Помоги развеять тьму и открыть путь для воды. #speaker:Эйлин #portrait:Princess
Сейчас около речки стоит волшебник; с его помощью ты сможешь развеять тьму. Только не обращай внимание на его манеры общения: у старых волшебников свои причуды.
Кем они являются на самом деле — загадка для меня, но они заслуживают доверия.
Мне кажется, они — воплощения этого волшебного места, будто сам цветок их создал, чтобы испытывать людей, проникать в их душу. И недостойный будет выгнан из этого места.
->END

= WaterCondition
Поздравляю, у тебя получилось провести путь для воды. Осталось пройти испытание у второго брата. У него всё должно быть проще. #speaker:Эйлин #portrait:Princess
->END

= CrateCondition
Ты смог поставить ящики на нужное место, осталось провести путь для воды. Без неё ты не активируешь силу цветка. #speaker:Эйлин #portrait:Princess
->END

= FinalCondition
У тебя получилось выполнить оба главных испытания. Скорее иди дальше, осталось совсем чуть-чуть. #speaker:Эйлин #portrait:Princess
->END