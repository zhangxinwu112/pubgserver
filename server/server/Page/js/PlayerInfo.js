var playerUIApp = new Vue({
            el: '#playerUIApp',
            data: {
				isShowPlayInfo:false,
				
				playerScoreList: 
				    [{
				    	"id": 13,
				    	"userId": 55,
				    	"bulletCount": 80,
				    	"lifeValue": 228,
				    	"fightScore": 35,
				    	"userName": "天涯"
				    }, {
				    	"id": 14,
				    	"userId": 56,
				    	"bulletCount": 80,
				    	"lifeValue": 0,
				    	"fightScore": 35,
				    	"userName": "天涯1"
				    }, {
				    	"id": 15,
				    	"userId": 57,
				    	"bulletCount": 80,
				    	"lifeValue": 0,
				    	"fightScore": 35,
				    	"userName": "天涯0"
				    }]
			
            },
            mounted() {
				
				
            },
            methods: {
               ClosePlayInfoWindow()
               {
					playerUIApp.isShowPlayInfo = false;
               },
			   AddPlayerLife(index,_userId)
			   {
				   	
					var addLifeValue = this.$refs.input1[index].value;
					if("undefined" == typeof addLifeValue  || addLifeValue==0)
					{
						mui.toast('命值不能为空，请重试！');
						return ;
					}
					var url = "uniwebview://AddPlayerLife?userId=" + _userId +"&addLifeValue="+addLifeValue + "&currentUser="+ userId;
					window.location.href = url;
			   	
			   },
			   handleChange(value) {
			   	//console.log(value);
			   },
				
            }
        })
		
	function GetRoomLifeInfoByUser(data)
	{
		var _json = JSON.stringify(data);
		 var o = JSON.parse(_json);
		 playerUIApp.playerScoreList = o;
	}
	
	function AddPlayerLifeMessageShow(message)
	{
		
		if(message==0)
		{
			mui.alert("命值增加成功");
		}else
		{
			mui.alert("您的命值不足，无法给别的玩家增加命值");
		}
		
	}
	
		
		
		