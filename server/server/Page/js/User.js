
//alert(new Date()).Format("yyyy-MM-dd hh:mm:ss.S"));
var userId;
var userName;
var roomId =-1;

var userType =-1;

var lat;
var lon;
var url;

var runState =-1;

var lifeUserName;

var lifeuserId;

	 	  
var app = new Vue({
            el: '#app',
            data: {
                cut: true,
				showFastMessageWindow:false,
				ShowManagerUI:false,
				showlife:false,
				dis:true,
				buttonName:"管理",
				timeShow:"游戏剩时：加载中...",
                msg: {
                    bulletValue: 80,
					bulletName: "弹量信息80/100",
                    scoreValue: 60,
					scoreName: "战绩信息60",
                    lifeValue: 90,
					 lifeName: "生命值90/100",
                },
				fastMessagecontent:["我来了","集合","注意隐蔽","卧倒","攻击","前进","后退","开火","冲锋"],
				
                list: [
                ],
				currentUser:"当前用户:管理员",
				roomName:"房间名称:房间123",
				inputContent:""
            },
            mounted() {
				
               setInterval(() => {
				  ChckeScope();
                }, 10000)
				
            },
            methods: {
                receive() {
                    let index = this.list.length
                    this.list.push({
                        name: `第${index}条的数据`
                    })
                },
                delete() {
                    this.list.splice(0, 1);
                },
                send() { //发送
						
						this.showFastMessageWindow =false;
						if (this.inputContent  == '') {
							alert('输入内容不能为空!');
							return ;
						}
					
						//var that =this;
						
						var requestUrl = url+"SendMessage/"+roomId+"|"+this.inputContent+"|"+
						userName +"|"+userType+"|"+userId;
						ShowCurrrentMessage();
						axios.get(requestUrl)
						  .then(function (response) {
							  //that.inputContent = "";
						    //mui.toast('操作成功!',{ duration:'long', type:'div' })
							 //mui.alert('操作成功');
						  })
						  .catch(function (error) {
						    console.log(error);
						  });
						  
						  
                },
                isShow() {
                    this.cut = !this.cut;
					this.showFastMessageWindow = false;
                },
				FastChange()
				{
					this.showFastMessageWindow =!this.showFastMessageWindow;
				},
				CloseMessageWindow()
				{
					this.showFastMessageWindow = false;
				},
				SelectMessage(message){
					this.inputContent = message;
				},
				OpenManagerPlayer()
				{
					//玩家
					if(userType==0)
					{
						playerUIApp.isShowPlayInfo = !playerUIApp.isShowPlayInfo;
						if(playerUIApp.isShowPlayInfo)
						{
							window.location.href = "uniwebview://ShowPlayInfo?userId=" + userId;
						}
					}
					else
					{
						appManager.ShowManagerUI = !appManager.ShowManagerUI;
						if(appManager.ShowManagerUI)
						{
							
							window.location.href = "uniwebview://GetRoomList?userId=" + userId;
						}
					}
					
					
				}
            }
        })
		
		function ChatMessage(message)
		{
			
			var content = message.name +":"+message.content + "   ["+message.time +"]";
			if(content.length>30)
			{
				content = content.substring(0,30);
			}
			app.inputContent ="";
			app.list.push({
                        name: content
                    })
					
		   
		   if(app.list.length>6)
		   {
			   app.list.splice(0, 1);
		   }
		}
		
		function SetLifeMesage(data)
		{
			app.dis = false;
			//玩家
			if(data.currentUser.userType == 0) {
				app.showlife = true;
				app.currentUser = "当前用户:"+data.currentUser.userName;
				app.roomName = "所在房间:"+data.room.name;
				
				app.msg.bulletValue = data.life.bulletCount;
				app.msg.bulletName = "弹量信息"+data.life.bulletCount+"/100";
				
				app.msg.scoreValue = data.life.fightScore;
				app.msg.scoreName = "战绩信息"+data.life.fightScore+"/100";
				
				app.msg.lifeValue = data.life.lifeValue;
				if(data.life.lifeValue<0){
					data.life.lifeValue = 0;
				}
				
				app.msg.lifeName = "生命值"+data.life.lifeValue+"/100";
				app.buttonName = "队友";
			}
			else{
				app.currentUser = "当前用户:管理员";
				app.showlife = false;
				app.buttonName = "管理";
			}
		
		}
		
		
		function SetValue(data)
		{
			
			url ="http://" + data.ip + ":8899/" ;
			//alert(data.currentUser);
			if("undefined" != typeof data.currentUser){ 
				this.userId = data.currentUser.userId;
				this.userName = data.currentUser.userName;
				this.userType = data.currentUser.userType;			
				this.lat = data.currentUser.lat;
				this.lon =data.currentUser.lon;
				
			}
			
			if("undefined" != typeof data.room && data.room!=null){
				
				this.roomId = data.room.id;
			}
			if("undefined" != typeof data.grounp && data.grounp!=null){
				
				this.runState = data.grounp.runState;		
			}
		
		}
		
		function GetNowFormatDate() {//获取当前时间
			var date = new Date();
			
			var seperator2 = ":";
			currentdate = 
					date.getHours()  + seperator2  + date.getMinutes()
					+ seperator2 + date.getSeconds();
			return currentdate;
		}
		
		function ShowCurrrentMessage()
		{
			var _name = this.userName;
			if(this.userType!=0)
			{
				_name = "系统管理员";
			}
			
			var json =
			{
				name:_name,
				content:this.app.inputContent,
				time:GetNowFormatDate()
			}
			ChatMessage(json);
			
		}
		
		//检查值
		function ChckeScope()
		{
			if(userType==0 && runState==0)
			{
				var myLngLat = new AMap.LngLat(this.lon,this.lat);
				if(!circle.contains(myLngLat)){
					var requestUrl = url+"SubtractLife/"+userId;
					axios.get(requestUrl)
					  .then(function (response) {
						
					  })
					  .catch(function (error) {
					    console.log(error);
					  });
				}
			}
		}
		//管理员加血
		function AddLife(addValue)
		{
			if(userType==1)
			{
				var requestUrl = url+"AddLife/"+this.lifeuserId+"|"+addValue;
				axios.get(requestUrl)
				  .then(function (response) {
					
				  })
				  .catch(function (error) {
					console.log(error);
				  });
				
			}
		}
		
		var t1;
		///倒计时,参数为秒
		function RemianTime(remianTime)
		{
			remianTime = remianTime/60;
			t1 = new Date();
			t1 = new Date(t1.valueOf() + (60 * 1000 * remianTime));
		
			var timer=setInterval(callTime,1000);
		}
		
		
		function callTime(){
		    //var t1="2020/02/27 22:00"
		    if(new Date(t1)-new Date()<=0){
		      clearInterval(timer);
			  app.timeShow = "游戏已结束";
		      //alert('计时完成');
		    }else{
		      var h=cd(t1, new Date(), 'h');
		      var m=cd(t1, new Date(), 'm');
		      var s=cd(t1, new Date(), 's');
			   app.timeShow = "游戏剩时："+h+'时'+m+'分'+s+'秒';
		      
		    }
	
		
		}
		
		/*
		2017-01-04 by xw
		获取倒计时 返回值数字
		
		t1 开始时 时间格式
		t2 结束时 时间格式
		tg 要获取的值 字符串
		  h 时
		  m 分
		  s 秒
		*/
		function cd(t1, t2, tg) {
		    //相差的毫秒数
		    var ms = Date.parse(t1) - Date.parse(t2);
		    var minutes = 1000 * 60;
		    var hours = minutes * 60;
		    
		    //求出除开天数，剩余的毫秒数
		  
		    var h = Math.floor(ms / hours);
		    ms %= hours;
		    var m = Math.floor(ms / minutes);
		    ms %= minutes;
		    var s = Math.floor(ms / 1000);
		    //返回所需值并退出函数
		    switch(tg){
		      case 'h' : return h;
		      case 'm' : return m;
		      case 's' : return s;
		    }
		}
		
		
		
		
		