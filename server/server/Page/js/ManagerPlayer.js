var appManager = new Vue({
            el: '#managerUIApp',
            data: {
                
				ShowManagerUI:false,
				isShowDetailInfo:false,
				
				list: 
				    [{
				    	"id": 523,
				    	"name": "战队",
				    	"child": [{
				    		"id": "55",
				    		"name": "天涯t"
				    	}, {
				    		"id": "56",
				    		"name": "天涯t1"
				    	}, {
				    		"id": "57",
				    		"name": "天涯t0"
				    	}]
				    }, {
				    	"id": 524,
				    	"name": "测试战队",
				    	"child": []
				    }],
					
					UserDetailInfo:
					{
						"id": 17,
						"userId": 59,
						"bulletCount": 80,
						"lifeValue": 50,
						"fightScore": 35,
						"userName": "天涯4"
					}
				
			
               
            },
            mounted() {
				
    
            },
            methods: {
               
				AddPlayerLife(index,_userId)
				{
					var addlifeValue = this.$refs.input[index].value;
					//alert(addlifeValue + ","+_userId);
					mui.toast('命值增加成功!')
					var requestUrl = url+"AddLife/"+addlifeValue+"|"+_userId;
					axios.get(requestUrl)
					  .then(function (response) {
						
					    // mui.toast('命值增加成功!')
						
					  })
					  .catch(function (error) {
					    console.log(error);
					  });
					
				},
				
				 ClosePlayInfoWindow()
				{
					playerUIApp.isShowPlayInfo = false;
				},
				handleChange(value) {
					console.log(value);
				},
				closeUIManager()
				{
					appManager.ShowManagerUI = false;
				},
				 ShowDetailInfo(_userId)
				{
					appManager.isShowDetailInfo = true;
					window.location.href = "uniwebview://ShowPlayerDetailInfoList?userId=" + _userId;
					
					
				},
				closeDetailInfoUI()
				{
					appManager.isShowDetailInfo = false;
				}
            }
        })
		
	
	function ShowRoomList( data)
	{
		var _json = JSON.stringify(data);
		 var o = JSON.parse(_json);
		 appManager.list = o;
	}
	
	function ShowPlayerDetailInfo(data)
	{
		var _json = JSON.stringify(data);
		 var o = JSON.parse(_json);
		 appManager.UserDetailInfo = o;
	}
		
		
		