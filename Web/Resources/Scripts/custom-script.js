var links = document.getElementsByTagName("link");
for(var linkIndex = 0; linkIndex < links.length; linkIndex++) {
	var link = links[linkIndex];
	if(link) {
		if(link.rel == 'icon')
			link.href = '/favicon.ico';
	}
}