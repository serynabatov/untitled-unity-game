//Этот диалог предназначен для главного НПС, который находится около клетки с трупом с флягой. С ним по сюжету игрок производит диалог впервые.
INCLUDE globals.ink
{mainVarCage: ->main.prePurpose|->main}

=== main ===
Наконец-то вы пришли <color=\#F8FF30>принц</color>, мы вас ждали. Принцесса в опасности, нет королевство в опасности, её высочество усыпили и она осталась в горящем замке. Нужно её спасти, иначе тьма захватит мир.  #speaker:Вадос #portrait:Green Mage 

Наши рыцари пытались её спасти, но они не смогли, слишком сильно заклятие, которое усыпило её, но с вашей помощью мы сможем развеять его.
 + [...]
 
 - Принцесса Эйлина сдерживала тьму, которая пытается поглотить королевство, но колдун наложил чары и усыпил принцессу.
 
Пока что этот лес находится под защитой, и, достав цветок "спящая принцесса", мы сможем пробудить Эйлину и она спасёт королевство от тьмы.
 ~ mainVarCage = true
  ->purpose
  
  = purpose
  Вам нужно пройти по одному испытанию у моих братьев, и тогда вы сможете наполнить флягу священной водой. Эта вода развеет тьму, которая окутала цветок, который пропудит Эйлину.
  Исследуйте пространство, и вы встретите моих братьев, поговорите с каждым из них, они вам расскажут всё необходимое и помогут получить цветок.
  ~ certificate = true
  -> END
  
  = prePurpose
Принц, что вы тут делаете?! Вам напомнить о ваших делах? #speaker:Вадос #portrait:Green Mage 
+ [Да]
->purpose
+ [Нет]
->END