//Этот диалог предназначен для главного НПС, который находится около клетки с трупом с флягой. С ним по сюжету игрок производит диалог впервые.
INCLUDE globals.ink
{mainVarCage: ->main.prePurpose|->main}

=== main ===
Вы пришли <color=\#F8FF30><b>принц</b></color>, мы вас ждали. Принцесса в опасности, нет королевство в опасности, её высочество уснула в горящем замке. Нужно её спасти, иначе тьма захватит кородлевство, превратит это цветущее место в пустыню.  #speaker:Адос #portrait:Green Mage 

Наши рыцари пытались её спасти, но заклятье слишком сильно, никто из них не вернулся.
 + [...]
 
 - Принцесса Эйлина сдерживала тьму, которая пытается поглотить королевство, но колдун наложил чары и пересилил принцессу.
 
Пока что этот лес находится под защитой от зла, и, достав цветок <color=\#F8FF30><b>нилурия</b></color>, мы сможем пробудить Эйлину и спасти королевство от тьмы.
 ~ mainVarCage = true
  ->purpose
  
  = purpose
  Вам нужно пройти по одному испытанию у моих братьев, и тогда вы сможете <color=\#F8FF30><b>наполнить флягу</b></color> священной водой. Эта вода поможет цветку расцвести. Только потом вы сможете использовать его, чтобы помочь Эйлине.
  Исследуйте пространство, и вы встретите моих братьев, поговорите с каждым из них, они вам расскажут всё необходимое и помогут. Желаю удачи и терпения принц, оно вам понадобится.
  ~ certificate = true
  -> END
  
  = prePurpose
Принц, что вы тут делаете?! Вам напомнить о ваших делах? #speaker:Адос #portrait:Green Mage 
+ [Да]
->purpose
+ [Нет]
->END