(function ($) {
    $.extend($.fn, {
        treeCombo:
        function (setting) {
            if (!setting) {
                setting = {};
            }
            var ps = $.extend({
                method: 'get',
                multiple: false,
                width: 186,
                url: '',
                prefix: ''
            }, setting);
            var that = this;
            var id = that.attr('id');
            var comboxId = id + '_ComboTree';
            function binding() {
                createCombox();
            }

            function createCombox() {
                var control = $('<select />').attr('id', comboxId)
                if (ps.multiple) {
                    control.prop('multiple', true);
                }
                that.after(control);
                var opts = $.extend(ps, {
                    onChange: onChange
                })
                control.combotree(ps);
            }
            function onChange(newvalue, oldvalue) {
                //var value = newvalue.concat(newvalue);
                //value = _.uniq(value);
                //过滤父节点
                if (ps.prefix != '') {
                    newvalue = _.filter(newvalue, function (item) {
                        return item.indexOf(ps.prefix) == -1;
                    })
                }
                that.val(newvalue.join(","));
                that.parents('form:first').validate().element('#' + id);
            }

            binding();
        }
    })
})(jQuery)