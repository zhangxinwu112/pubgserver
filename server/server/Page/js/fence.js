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
				//console.log("end");
				console.log(circle.getCenter());
				console.log(circle.getRadius());
			
				circleEditor.close();
				
				
			},
		}
    });
	//var lon = 108.950543;
	//var lat = 34.199175;
	var map;
	var circleEditor;
	var circle;
  

	CreateMap(108.950543,34.199175,4000,12);

	function CreateMap(lon,lat,radius,grounpId)
	{
		map = new AMap.Map('container', {
		resizeEnable: true,
		center: [lon, lat],
		showLabel:true,
		mapStyle: "amap://styles/whitesmoke",
		zoom: 13
		});
		
		createCircle(lon,lat,radius);
		
	}

  
 
   function createCircle(lon,lat,radius)
   {
	    circle = new AMap.Circle({
	           center: [lon, lat],
	           radius: radius, //半径
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
			  
		   })
	   
		   circleEditor.on('end', function(event) {
			 
			   // event.target 即为编辑后的圆形对象
		   })
		   
		   circleEditor.open();
			   
   }
	
	
	function Back() {
		var num1 = 1;
		window.location.href = "uniwebview://Back?num1=" + num1;
	}
	
	