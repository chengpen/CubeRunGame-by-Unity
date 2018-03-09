--[定义变量]
local transform;
local gameObject;
--[定义表]
SettingPanel={};
local this =SettingPanel;

--[Awake方法]
function  SettingPanel.Awake(obj)
transform=obj.transform;
gameObject=obj;
this.InitPanel();
end

--[初始化面板，查找游戏物体组件]
function SettingPanel.InitPanel()
this.btnHelp=transform:Find("help").gameObject;
this.btnShop=transform:Find("shop").gameObject;
this.Parent=GameObject.Find("Start_UI").transform;
end