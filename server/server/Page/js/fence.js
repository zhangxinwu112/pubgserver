    	mui.init();
	var app=new Vue({
    
    	el:"#app",
    	data:{
    		
    		buttonShow:false
    	},
		methods:{
			StartEdit:function(){
				this.buttonShow= !this.buttonShow;
				circleEditor.open();
				
			},
			EndEdit:function(){
				this.buttonShow= !this.buttonShow;
				 
				circleEditor.close();
				var requestUrl = url+"SaveFence/"+_grounpId+"|"+
				circle.getCenter().getLng()+"|"+circle.getCenter().getLat()+"|"+circle.getRadius();
				mui.alert('操作成功');
				axios.get(requestUrl)
				  .then(function (response) {
				    //mui.toast('操作成功!',{ duration:'long', type:'div' })
					 //mui.alert('操作成功');
				  })
				  .catch(function (error) {
				    console.log(error);
				  });
				  
				  
				
				
			},
		}
    });
	//var lon = 108.950543;
	//var lat = 34.199175;
	var map;
	var circleEditor;
	var circle;
	var _grounpId;
	var url;
	var fenceRadius;
	var json ={"lon":108.950544,lat:34.199176,"grounpId":9,"ip":"192.168.1.6","fenceRadius":1000};
	CreateMap(json);

	function CreateMap(json)
	{
		 _grounpId = json.grounpId;
		 url ="http://" + json.ip + ":8899/" ;
		 //console.log(url);
		 fenceRadius =json.fenceRadius;
		map = new AMap.Map('container', {
		resizeEnable: true,
		center: [json.lon, json.lat],
		showLabel:true,
		mapStyle: "amap://styles/whitesmoke",
		zoom: 13
		});
		
		createCircle(json);
		
	}

  
 
   function createCircle(json)
   {
	  
	    circle = new AMap.Circle({
	           center: [json.lon, json.lat],
	           radius: fenceRadius, //半径
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
		       // 缩放地图到合适的视野级别
		   map.setFitView([ circle ])
	   
		   circleEditor = new AMap.CircleEditor(map, circle)
		  
	   
		   circleEditor.on('move', function(event) {
			   
		   })
	   
		   circleEditor.on('adjust', function(event) {
			   var grounpId = json.grounpId; 
		   })
	   
		   circleEditor.on('end', function(event) {
			 
			   // event.target 即为编辑后的圆形对象
			    var grounpId = json.grounpId; 
		   })
		   
		   circleEditor.open();
		  
   }
	
	
	function Back() {
		var num1 = 1;
		window.location.href = "uniwebview://Back?num1=" + num1;
	}
	
	