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
                list: [{
                        name: '88888888'
                    },
                    {
                        name: '2222222222222222'
                    },
                    {
                        name: '333333333333333'
                    },
                    {
                        name: '444444444444'
                    },
                    {
                        name: '55555555555555'
                    },{
                        name: '55555555555555'
                    }
                ],
				currentUser:"当前用户:我的天涯",
				roomName:"房间名称:房间123"
            },
            mounted() {
               /** setInterval(() => {
                    this.receive()
                    this.delete()
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
		

