var app = new Vue({
            el: '#app',
            data: {
                cut: true,
				showlife:false,
                msg: {
                    bulletValue: 80,
					bulletName: "弹量信息80/100",
                    scoreValue: 60,
					scoreName: "战绩信息60",
                    lifeValue: 90,
					 lifeName: "生命值90/100",
                },
                list: [
                ],
				currentUser:"当前用户:管理员",
				roomName:"房间名称:房间123"
            },
            mounted() {
				/**
               setInterval(() => {
                   // this.receive()
                   // this.delete()
				   showMessage("123");
                }, 2000)
				
				**/
				
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

                },
                isShow() {
                    this.cut = !this.cut
                }
            }
        })
		
		function ChatMessage(message)
		{
			app.list.push({
                        name: message
                    })
					
		   
		   if(app.list.length>6)
		   {
			   app.list.splice(0, 1);
		   }
		}
		
		function SetLifeMesage(data)
		{
			if(data.currentUser.userType != -1) {
				app.showlife = true;
				app.currentUser = "当前用户:"+data.currentUser.userName;
				app.roomName = "所在房间:"+data.room.name;
				
				app.msg.bulletValue = data.life.bulletCount;
				app.msg.bulletName = "弹量信息"+data.life.bulletCount+"/100";
				
				app.msg.scoreValue = data.life.fightScore;
				app.msg.scoreName = "战绩信息"+data.life.fightScore+"/100";
				
				app.msg.lifeValue = data.life.lifeValue;
				app.msg.lifeName = "生命值"+data.life.lifeValue+"/100";
			}
			else{
				app.currentUser = "当前用户:管理员";
				app.showlife = false;
			}
			
		
		}
		
		
		

