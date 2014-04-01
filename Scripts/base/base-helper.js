(function () {
  var JHelper = {
    //设置页面内容高度
    initLeftMenu: function (e) {
      var self = this;
      $('.easyui-accordion li a').click(function (e) {
        e.preventDefault();
        var target = $(e.currentTarget);
        var tabTitle = target.text();
        var url = target.attr("href");
        self.addTab(tabTitle, url);
        target.parent().addClass("selected");
      })
    },
    addTab: function (subtitle, url) {
      var self = this;
      if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
          title: subtitle,
          content: self.createFrame(url),
          //href: url,
          closable: true,
          //width: $('#mainPanle').width() - 10,
          //height: $('#mainPanle').height() - 26
        });
        var tab = $('#tabs').tabs('getTab', subtitle);
        tab.css("overflow", "hidden");
      } else {
        $('#tabs').tabs('select', subtitle);
      }
      self.tabClose();
    },
    createFrame: function (url) {
      var s = '<iframe frameborder="0" scrolling="no" name="mainFrame" src="' + url + '" style="width:100%;height:100%; margin:0;padding:0;border:0"></iframe>';
      return s;
    },


    tabClose: function () {
      $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children("span").text();
        $('#tabs').tabs('close', subtitle);
      })

      $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
          left: e.pageX,
          top: e.pageY,
        });

        var subtitle = $(this).children("span").text();
        $('#mm').data("currtab", subtitle);

        return false;
      });
    },
    tabCloseEven: function () {
      //关闭当前
      $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
      })

      $('#mm-tabrefresh').click(function () {
        var currtab_title = $('#mm').data("currtab");
        var tab = $('#tabs').tabs('getTab', currtab_title);
        tab.panel('refresh');
      })
      //全部关闭
      $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
          var t = $(n).text();
          $('#tabs').tabs('close', t);
        });
      });
      //关闭除当前之外的TAB
      $('#mm-tabcloseother').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('.tabs-inner span').each(function (i, n) {
          var t = $(n).text();
          if (t != currtab_title)
            $('#tabs').tabs('close', t);
        });
      });
      //关闭当前右侧的TAB
      $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
          //msgShow('系统提示','后边没有啦~~','error');
          alert('后边没有啦~~');
          return false;
        }
        nextall.each(function (i, n) {
          var t = $('a:eq(0) span', $(n)).text();
          $('#tabs').tabs('close', t);
        });
        return false;
      });
      //关闭当前左侧的TAB
      $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
          alert('到头了，前边没有啦~~');
          return false;
        }
        prevall.each(function (i, n) {
          var t = $('a:eq(0) span', $(n)).text();
          $('#tabs').tabs('close', t);
        });
        return false;
      });

      //退出
      $("#mm-exit").click(function () {
        $('#mm').menu('hide');
      })
    },
    showMessage: function (res) {
      if (res.Success) {
        $.messager.show({
          title: '操作提示',
          msg: res.Message,
          timeout: 1000,
          showType: 'slide'
        });
      } else {
        $.messager.alert('操作失败', res.Message, 'error');
      }
    }
  }
  window.Maitonn = window.Maitonn || {};
  window.Maitonn.JHelper = JHelper;
  $(function () {
    Maitonn.JHelper.initLeftMenu();
    Maitonn.JHelper.tabCloseEven();
  })
})()