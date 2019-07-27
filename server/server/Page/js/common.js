
var radios = document.querySelectorAll("#map-styles input");
             radios.forEach(function(ratio) {
             ratio.onclick = setMapStyle;
        });

        function setMapStyle() {
             var styleName = "amap://styles/" + this.value;
             map.setMapStyle(styleName);
        }
