Map_big_test = Map()
Map_big_test.Ground = Create:Ground("big_test")
Element_Torch_67310 = Create:Element("Torch")
Element_Torch_67310:SetMap(Map_big_test, 347, 190)
EBB_1 = EBoundingBox ( Element_Torch_67310, EBoundingBoxType.Event, 0, 77, 70, 150 )
EBB_1_Event_1 = ObjectEvent ( ObjectEventType.Normal, true, InputType.Action )
EBB_1_Event_1_Action_1 = WarpAction ( "big_test", "fir" )
EBB_1_Event_1:AddAction ( EBB_1_Event_1_Action_1 )
EBB_1:AddEvent ( EBB_1_Event_1 )
Element_Torch_67310:AddEventBoundingBox( EBB_1, EventBoundingBoxType.External )
Element_DeadTree_67317 = Create:Element("DeadTree")
Element_DeadTree_67317:SetMap(Map_big_test, 188, 211)
Element_DeadTree_67320 = Create:Element("DeadTree")
Element_DeadTree_67320:SetMap(Map_big_test, 238, 321)
Element_DeadTree_67323 = Create:Element("DeadTree")
Element_DeadTree_67323:SetMap(Map_big_test, 119, 361)
Element_DeadTree_67326 = Create:Element("DeadTree")
Element_DeadTree_67326:SetMap(Map_big_test, 432, 417)
Element_DeadTree_67329 = Create:Element("DeadTree")
Element_DeadTree_67329:SetMap(Map_big_test, 521, 183)
Element_DeadTree_67332 = Create:Element("DeadTree")
Element_DeadTree_67332:SetMap(Map_big_test, 412, 65)
Element_DeadTree_67335 = Create:Element("DeadTree")
Element_DeadTree_67335:SetMap(Map_big_test, 161, 26)
Element_BrownStatue_177269 = Create:Element("BrownStatue")
Element_BrownStatue_177269:SetMap(Map_big_test, 31, 68)
Element_BrownStatue_177272 = Create:Element("BrownStatue")
Element_BrownStatue_177272:SetMap(Map_big_test, 304, 458)
Element_BrownStatue_177275 = Create:Element("BrownStatue")
Element_BrownStatue_177275:SetMap(Map_big_test, 607, 137)
Element_BrownStatue_177278 = Create:Element("BrownStatue")
Element_BrownStatue_177278:SetMap(Map_big_test, 586, 257)
Element_StoneStatue_177287 = Create:Element("StoneStatue")
Element_StoneStatue_177287:SetMap(Map_big_test, 284, 225)
Element_StoneStatue_177290 = Create:Element("StoneStatue")
Element_StoneStatue_177290:SetMap(Map_big_test, 443, 237)
Element_StoneStatue_177293 = Create:Element("StoneStatue")
Element_StoneStatue_177293:SetMap(Map_big_test, 363, 366)
Element_SilverPile_177301 = Create:Element("SilverPile")
Element_SilverPile_177301:SetMap(Map_big_test, 547, 43)
Map_big_test:AddWarpPoint("Default", Vector2 ( 133, 171 ), Direction.S)
Map_big_test:AddWarpPoint("sec", Vector2 ( 5433, 5618 ), Direction.NO)
Map_big_test:AddWarpPoint("fir", Vector2 ( 118, 273 ), Direction.N)
return Map_big_test