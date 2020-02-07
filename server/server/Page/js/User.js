var app = new Vue({
            el: '#app',
            data: {
                cut: true,
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
				currentUser:"当前用户:我的天涯",
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
		
		function showMessage(message)
		{
			app.list.push({
                        name: message
                    })
					
		   
		   if(app.list.length>6)
		   {
			   app.list.splice(0, 1);
		   }
		}
		

