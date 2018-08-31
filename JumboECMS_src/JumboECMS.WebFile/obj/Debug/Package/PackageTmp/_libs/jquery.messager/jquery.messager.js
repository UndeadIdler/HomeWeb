(function(jQuery) {
    this.layer = {
        'width': 200,
        'height': 24
    };
    this.title = '1';
    this.sfuc = null;
    this.time = 4000;
    this.anims = {
        'type': 'slide',
        'speed': 1000,
        'root': '/message/'
    };
    this.inits = function(title, text) {
        if ($("#PopTips").is("div")) {
            return
        };
        var sBody = '<div id="PopTips" style="z-index:100;width:90%;margin:0px auto;text-align:center;height:' + (this.layer.height + 2) + 'px;position:absolute;display:none; top:2px;"><div id="tips_Content" style="margin:0px auto;font-size:12px;width:' + (this.layer.width) + 'px;height:' + (this.layer.height) + 'px;border:#000000 1px solid;color:#000000;background:#ffffcc;text-align:left;"><span style="background:url(' + this.anims.root + 'statics/tips/' + this.title + '.gif) 1px no-repeat; text-indent:25px;float:left;height:16px;line-height:16px;padding-top:6px;">' + text + '</span><span id="PopTips_Close" style="float:right; padding-right:2px;width:16px;line-height:auto;color:red;font-size:12px;font-weight:bold;text-align:center;cursor:pointer;overflow:hidden;padding-top:6px;">×</span></div></div>';
        $(document.body).prepend(sBody)
    };
    this.show = function(title, text, time) {
        if ($("#PopTips").is("div")) {
            return
        };
        if (title == '0' || !title) title = this.title;
        this.inits(title, text);
        if (time) this.time = time;
        switch (this.anims.type) {
        case 'slide':
            $("#PopTips").slideDown(this.anims.speed);
            break;
        case 'fade':
            $("#PopTips").fadeIn(this.anims.speed);
            break;
        case 'show':
            $("#PopTips").show(this.anims.speed);
            break;
        default:
            $("#PopTips").slideDown(this.anims.speed);
            break
        }
        $("#PopTips_Close").click(function() {
            setTimeout('this.close()', 1)
        });
        this.rmtips(this.time)
    };
    this.lays = function(width, height) {
        if ($("#PopTips").is("div")) {
            return
        };
        if (width != 0 && width) this.layer.width = width;
        if (height != 0 && height) this.layer.height = height
    };
    this.anim = function(type, speed, root) {
        if ($("#PopTips").is("div")) {
            return
        };
        if (type != 0 && type) this.anims.type = type;
        if (speed != 0 && speed) {
            switch (speed) {
            case 'slow':
                this.anims.speed = 1000;
                break;
            case 'fast':
                this.anims.speed = 200;
                break;
            case 'normal':
                this.anims.speed = 400;
                break;
            default:
                this.anims.speed = speed
            }
        };
        if (root) {
            this.anims.root = root;
        }
    };
    this.rmtips = function(time) {
        setTimeout('this.close()', time)
    };
    this.close = function() {
        if ($("#PopTips").is("div")) {
            switch (this.anims.type) {
            case 'slide':
                $("#PopTips").slideUp(this.anims.speed);
                break;
            case 'fade':
                $("#PopTips").fadeOut(this.anims.speed);
                break;
            case 'show':
                $("#PopTips").hide(this.anims.speed);
                break;
            default:
                $("#PopTips").slideUp(this.anims.speed);
                break
            };
            setTimeout('$("#PopTips").remove();', this.anims.speed);
            if (this.sfuc != null) eval(this.sfuc);
            this.original()
        }
    };
    this.original = function() {
        this.layer = {
            'width': 200,
            'height': 24
        };
        this.title = '1';
        this.sfuc = null;
        this.time = 4000;
        this.anims = {
            'type': 'slide',
            'speed': 1000,
            'root': '/message/'
        }
    };
    this.doafter = function(_sFuc) {
        this.sfuc = _sFuc
    };
    jQuery.message = this;
    return jQuery
})(jQuery);