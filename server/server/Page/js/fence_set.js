var app1=new Vue({
    
    	el:"#app1",
    	data:{
    		
    		radius:1000
    	},
		methods:{
			SaveRadius:function(){
				circle.setRadius(this.radius);
				
			},
		}
    });
	

function SetRadius(_radius)
{
	app1.radius = _radius;
}
	
	

