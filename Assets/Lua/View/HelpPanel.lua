local transform;
local gameObject;

HelpPanel={};
local this =HelpPanel;

function HelpPanel.Awake(obj)
gameObject=obj;
transform=obj.transform;
this.InitPanel();
end


function HelpPanel.InitPanel()
this.Parent=GameObject.Find("Start_UI").transform;
this.btnClose=transform:Find("Closebtn").gameObject;
end