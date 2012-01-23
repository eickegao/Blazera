NPC_Vlad = NPC()
NPC_Vlad:AddBoundingBox(BBoundingBox(NPC_Vlad, 0, 15, 17, 30))
NPC_Vlad:AddBodyBoundingBox(EBoundingBox(NPC_Vlad, EBoundingBoxType.Body, 0, 15, 17, 30))
NPC_Vlad:AddSkin("Inactive", Direction.N, Create:Animation("Vlad_N_N"))
NPC_Vlad:AddSkin("Inactive", Direction.S, Create:Animation("Vlad_S_N"))
NPC_Vlad:AddSkin("Inactive", Direction.E, Create:Animation("Vlad_E_N"))
NPC_Vlad:AddSkin("Inactive", Direction.O, Create:Animation("Vlad_O_N"))
NPC_Vlad:AddSkin("Inactive", Direction.NE, Create:Animation("Vlad_NE_N"))
NPC_Vlad:AddSkin("Inactive", Direction.NO, Create:Animation("Vlad_NO_N"))
NPC_Vlad:AddSkin("Inactive", Direction.SE, Create:Animation("Vlad_SE_N"))
NPC_Vlad:AddSkin("Inactive", Direction.SO, Create:Animation("Vlad_SO_N"))
NPC_Vlad:AddSkin("Moving", Direction.N, Create:Animation("Vlad_N_W"))
NPC_Vlad:AddSkin("Moving", Direction.S, Create:Animation("Vlad_S_W"))
NPC_Vlad:AddSkin("Moving", Direction.E, Create:Animation("Vlad_E_W"))
NPC_Vlad:AddSkin("Moving", Direction.O, Create:Animation("Vlad_O_W"))
NPC_Vlad:AddSkin("Moving", Direction.NE, Create:Animation("Vlad_NE_W"))
NPC_Vlad:AddSkin("Moving", Direction.NO, Create:Animation("Vlad_NO_W"))
NPC_Vlad:AddSkin("Moving", Direction.SE, Create:Animation("Vlad_SE_W"))
NPC_Vlad:AddSkin("Moving", Direction.SO, Create:Animation("Vlad_SO_W"))
NPC_Vlad:AddSkin("Running", Direction.N, Create:Animation("Vlad_N_R"))
NPC_Vlad:AddSkin("Running", Direction.S, Create:Animation("Vlad_S_R"))
NPC_Vlad:AddSkin("Running", Direction.E, Create:Animation("Vlad_E_R"))
NPC_Vlad:AddSkin("Running", Direction.O, Create:Animation("Vlad_O_R"))
NPC_Vlad:AddSkin("Running", Direction.NE, Create:Animation("Vlad_NE_R"))
NPC_Vlad:AddSkin("Running", Direction.NO, Create:Animation("Vlad_NO_R"))
NPC_Vlad:AddSkin("Running", Direction.SE, Create:Animation("Vlad_SE_R"))
NPC_Vlad:AddSkin("Running", Direction.SO, Create:Animation("Vlad_SO_R"))
return NPC_Vlad