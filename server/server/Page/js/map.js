
//mui.alert('拖动圆的瞄点,调整圆形电子围栏的位置和大小.');
//mui("#demo1").progressbar({progress:20}).show();
var _data ={
	
		"currentUser": {
			"userId": 15,
			"userName": "\u5929\u6DAF",
			"lon": 108.963393,
			"lat": 34.274106,
			"userType": 1,
			"color": "#CC33FF"
		},
		"gpsData": [{
			"userId": 15,
			"userName": "\u5929\u6DAF",
			"lon": 108.963393,
			"lat": 34.274106,
			"userType": 0,
			"color": "#CC33FF"
		},
		{
			"userId": 16,
			"userName": "测试",
			"lon": 108.953393,
			"lat": 34.244106,
			"userType": 0,
			"color": "#CC33FF"
		}
		
		],
		"grounp": {
			"area": "cs",
			"userId": 16,
			"runState": 0,
			"playerTime": 60,
			"fenceLon": 108.9637,
			"fenceLat": 34.17328,
			"fenceRadius": 801,
			"isDefence": false,
			"id": 109,
			"name": "\u897F\u5B89\u56E2\u961F"
		},
		"room": {
			"grounpId": 109,
			"code": 2,
			"checkCode": "123456",
			"userCount": 0,
			"isCurrentUser": false,
			"id": 483,
			"name": "\u6211\u7684\u623F\u95F4"
		},
		"life": {
			"id": 2,
			"userId": 15,
			"bulletCount": 200,
			"lifeValue": 50,
			"fightScore": 36
		},
		"ip": "192.168.1.4"
	}

        var map;
        var markArray = new Array(); 
		
        //CreateMapAndMarker(_data);
        function CreateMapAndMarker(data)
        {
           // console.log(data);
		  
		   // var data = JSON.parse(str);
			SetLifeMesage(data);
			
			SetValue(data);
			
            if(!map)
            {
                CreateMap(data.currentUser.lon,data.currentUser.lat);
                SetHander();
            }
            else
            {
                map.center = [data.currentUser.lon, data.currentUser.lat];
            }
            DeleteMark();
            AddMarker(data.gpsData);
			
			CreateCircle(data.grounp);
           
        }
		
		

        
        function CreateMap(lon,lat)
        {
            map = new AMap.Map('container', {
            resizeEnable: true,
            center: [lon, lat],
            showLabel:true,
            mapStyle: "amap://styles/whitesmoke",
            zoom: 13
            });
             // 构造官方卫星、路网图层
   		    //var satelliteLayer = new AMap.TileLayer.Satellite();
    		//var roadNetLayer =  new AMap.TileLayer.RoadNet();

    		//批量添加图层
   		    //map.add([satelliteLayer, roadNetLayer]);
        }
		
		var circle;
		function CreateCircle(grounp)
		{
			if(grounp.fenceLon>0)
			{
				if(circle==null)
				{
					circle = new AMap.Circle({
					       center: [grounp.fenceLon, grounp.fenceLat],
					       radius: grounp.fenceRadius, //半径
					       borderWeight: 3,
					       strokeColor: "#FF33FF", 
					       strokeOpacity: 1,
					       strokeWeight: 6,
					       strokeOpacity: 0.2,
					       fillOpacity: 0.4,
					       strokeStyle: 'dashed',
					       strokeDasharray: [10, 10], 
					       // 线样式还支持 'dashed'
					       fillColor: '#1791fc',
					       zIndex: 50,
					   })
					   
					   circle.setMap(map)
				}
				else
				{
					if(grounp.fenceRadius<=0)
					{
						circle.setRadius(0.1);
					}
					else{
						circle.setRadius(grounp.fenceRadius);
					}
					
				}
				
			}
			
		}

        function AddMarker(data)
        // `<i class="content">111</i>`
        {
            data.forEach(function (_item) {
                var _markObject = new AMap.Marker({
                map: map,
                icon: _item.icon,
                content: `<i class="content" style="background:${_item.color};">${_item.userName}</i>`,
                // direction: marker.direction,
                position: [_item.lon, _item.lat],
                offset: new AMap.Pixel(-13, -30)
				
            });
			if(userType==1)
			{
				var clickHandle = AMap.event.addListener(_markObject, 'click', function(e) {
				    this.lifeUserName = _item.userName;
					  this.lifeuserId = _item.userId;
					  alert(this.lifeuserId);
				  });
			}
           
          markArray.push(_markObject);
		
			
         
         });
        }

        function DeleteMark()
        {
           for (var i = 0; i < markArray.length; i ++) {
                markObject = markArray[i];
                if (markObject) {
                    markObject.setMap(null);
                    markObject = null;
                }
            }
           
            markArray =new Array();
        }

        function SetHander()
        {
           
                // 添加事件监听, 使地图自适应显示到合适的范围
                AMap.event.addDomListener(document.getElementById('setFitView'), 'click', function () {
                    var newCenter = map.setFitView();
                   // document.getElementById('centerCoord').innerHTML = '当前中心点坐标：' + newCenter.getCenter();
                    //document.getElementById('tips').innerHTML = '通过setFitView，地图自适应显示到合适的范围内,点标记已全部显示在视野中！';
                });
        }
		
		function Back() {
		//alert("122");
			var num1 = 1;
			//var num2 = 2;
			window.location.href = "uniwebview://Back?num1=" + num1;
		}
		
		function ConfirmGameOver() {
		//alert("122");
			var num1 = 1;
			//var num2 = 2;
			window.location.href = "uniwebview://GameOver?num1=" + num1;
		}
		
		function StartGameOver() {
		
			var btnArray = ['是'];
			    var message = '本局游戏结束,确定后返回战绩界面.';
			    mui.confirm(message, '信息提示', btnArray, function(e) {
			       
			          ConfirmGameOver();
			        
			    },'div');
			
		}