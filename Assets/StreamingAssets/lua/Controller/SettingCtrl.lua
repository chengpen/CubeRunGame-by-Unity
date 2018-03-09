require "Controller/HelpCtrl"

--[定义表]
SettingCtrl={};
local this =SettingCtrl;

--[定义变量]

local transform;
local gameObject;
local setting;

--[实例化]
function SettingCtrl.New()
return this ;
end


function SettingCtrl.Awake()
--[创建面板并传递回调函数]
 PanelManager:LoadAB('GameAtlas');
 PanelManager:CreatePanel('Setting',this.OnCreate);
end

--[回调函数用于创建面板时进行初始化]
function SettingCtrl.OnCreate(obj)
gameObject = obj;
transform=obj.transform;
transform:SetParent(SettingPanel.Parent);
setting = gameObject:GetComponent('LuaBehaviour');
setting:AddClick(SettingPanel.btnHelp,this.OnClick);
setting:AddClick(SettingPanel.btnShop,this.OnShopClick);
end

--[按钮单击事件]
function SettingCtrl.OnClick(go)
HelpCtrl.Awake();
end

function SettingCtrl.OnShopClick(go)
warn("----ShopClick-----");
end