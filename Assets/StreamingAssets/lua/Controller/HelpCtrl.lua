HelpCtrl={};
local this =HelpCtrl;

local transform;
local gameObject;
local help;


function HelpCtrl.New()
return this ;
end

function HelpCtrl.Awake()
PanelManager:CreatePanel('Help',this.OnCreate);
end

function HelpCtrl.OnCreate(obj)
gameObject = obj;
transform=obj.transform;
transform:SetParent(HelpPanel.Parent);
help = gameObject:GetComponent('LuaBehaviour');
help:AddClick(HelpPanel.btnClose,this.OnClick);
end


function HelpCtrl.OnClick()
destroy(gameObject);
end