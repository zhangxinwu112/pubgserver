

var _data ={
	"currentUser": {
		"userId": 15,
		"userName": "\u5929\u6DAF",
		"lon": 108.964236948926,
		"lat": 34.1732384409584,
		"userType": 0
	},
	"gpsData": [{
		"userId": 15,
		"userName": "\u5929\u6DAF",
		"lon": 108.964236948926,
		"lat": 34.1732384409584,
		"userType": 0
	}, {
		"userId": 18,
		"userName": "\u73A9\u5BB6\u6D4B\u8BD5",
		"lon": 108.995864758826,
		"lat": 34.1731979651086,
		"userType": 0
	}],
	"grounp": {
		"area": "cs",
		"userId": 16,
		"runState": 0,
		"playerTime": 60,
		"fenceLon": -1,
		"fenceLat": -1,
		"fenceRadius": 2000,
		"id": 105,
		"name": "122121"
	}
}
        var map;
        var markArray = new Array(); 
        CreateMapAndMarker(_data);
        function CreateMapAndMarker(str)
        {
           // console.log(data);
		     var data = JSON.parse(str);
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
					circle.setRadius(grounp);
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
                content: `<i class="content" >${_item.userName}</i>`,
                // direction: marker.direction,
                position: [_item.lon, _item.lat],
                offset: new AMap.Pixel(-13, -30)
            });
           
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
		
		function Exit() {
		//alert("122");
			var num1 = 1;
			//var num2 = 2;
			window.location.href = "uniwebview://Exit?num1=" + num1;
		}