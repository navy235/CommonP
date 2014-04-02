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
        prefix: '',
        value: ''
      }, setting);
      var that = this;
      var id = that.attr('id');
      var comboxId = id + '_ComboTree';
      var currentValue = ps.value.split(',');
      function binding() {
        createComboTree();
      }

      function createComboTree() {
        var control = $('<select />').attr('id', comboxId)
        if (ps.multiple) {
          control.prop('multiple', true);
        }
        that.after(control);
        var opts = $.extend(ps, {
          onChange: onChange
        })
        opts.value = null;
        if (currentValue.length > 0) {
          opts.setValues = currentValue;
        }
        control.combotree(opts);

      }

      function onChange(newvalue, oldvalue) {
        //var value = newvalue.concat(newvalue);
        //value = _.uniq(value);
        //过滤父节点
        if (ps.prefix != '') {

          var valArr = [];
          $.each(newvalue, function (index, item) {
            if (item.indexOf(ps.prefix) == -1) {
              valArr.push(item);
            }
          })
        }
        that.val(valArr.join(","));
        that.parents('form:first').validate().element('#' + id);
      }

      binding();
    }
  })
})(jQuery)