﻿/*
 Highcharts JS v9.3.2 (2021-11-29)

 Accessibility module

 (c) 2010-2021 Highsoft AS
 Author: Oystein Moseng

 License: www.highcharts.com/license
*/
'use strict'; (function (a) { "object" === typeof module && module.exports ? (a["default"] = a, module.exports = a) : "function" === typeof define && define.amd ? define("highcharts/modules/accessibility", ["highcharts"], function (x) { a(x); a.Highcharts = x; return a }) : a("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (a) {
    function x(a, h, t, r) { a.hasOwnProperty(h) || (a[h] = r.apply(null, t)) } a = a ? a._modules : {}; x(a, "Accessibility/A11yI18n.js", [a["Core/FormatUtilities.js"], a["Core/Utilities.js"]], function (a, h) {
        var k = a.format,
        r = h.pick, m; (function (a) {
            function m(a, c) {
                var e = a.indexOf("#each("), d = a.indexOf("#plural("), b = a.indexOf("["), f = a.indexOf("]"); if (-1 < e) { f = a.slice(e).indexOf(")") + e; d = a.substring(0, e); b = a.substring(f + 1); f = a.substring(e + 6, f).split(","); e = Number(f[1]); a = ""; if (c = c[f[0]]) for (e = isNaN(e) ? c.length : e, e = 0 > e ? c.length + e : Math.min(e, c.length), f = 0; f < e; ++f)a += d + c[f] + b; return a.length ? a : "" } if (-1 < d) {
                    b = a.slice(d).indexOf(")") + d; d = a.substring(d + 8, b).split(","); switch (Number(c[d[0]])) {
                        case 0: a = r(d[4], d[1]); break; case 1: a =
                            r(d[2], d[1]); break; case 2: a = r(d[3], d[1]); break; default: a = d[1]
                    }a ? (c = a, c = c.trim && c.trim() || c.replace(/^\s+|\s+$/g, "")) : c = ""; return c
                } return -1 < b ? (d = a.substring(0, b), b = Number(a.substring(b + 1, f)), a = void 0, c = c[d], !isNaN(b) && c && (0 > b ? (a = c[c.length + b], "undefined" === typeof a && (a = c[0])) : (a = c[b], "undefined" === typeof a && (a = c[c.length - 1]))), "undefined" !== typeof a ? a : "") : "{" + a + "}"
            } function n(a, c, e) {
                var d = function (b, d) {
                    b = b.slice(d || 0); var f = b.indexOf("{"), c = b.indexOf("}"); if (-1 < f && c > f) return {
                        statement: b.substring(f +
                            1, c), begin: d + f + 1, end: d + c
                    }
                }, b = [], f = 0; do { var u = d(a, f); var v = a.substring(f, u && u.begin - 1); v.length && b.push({ value: v, type: "constant" }); u && b.push({ value: u.statement, type: "statement" }); f = u ? u.end + 1 : f + 1 } while (u); b.forEach(function (b) { "statement" === b.type && (b.value = m(b.value, c)) }); return k(b.reduce(function (b, d) { return b + d.value }, ""), c, e)
            } function g(a, c) { a = a.split("."); for (var e = this.options.lang, d = 0; d < a.length; ++d)e = e && e[a[d]]; return "string" === typeof e ? n(e, c, this) : "" } var q = []; a.compose = function (a) {
                -1 === q.indexOf(a) &&
                (q.push(a), a.prototype.langFormat = g); return a
            }; a.i18nFormat = n
        })(m || (m = {})); return m
    }); x(a, "Accessibility/Utils/HTMLUtilities.js", [a["Core/Globals.js"], a["Core/Utilities.js"]], function (a, h) {
        function k(a) {
            if ("function" === typeof w.MouseEvent) return new w.MouseEvent(a.type, a); if (m.createEvent) {
                var n = m.createEvent("MouseEvent"); if (n.initMouseEvent) return n.initMouseEvent(a.type, a.bubbles, a.cancelable, a.view || w, a.detail, a.screenX, a.screenY, a.clientX, a.clientY, a.ctrlKey, a.altKey, a.shiftKey, a.metaKey, a.button,
                    a.relatedTarget), n
            } return r(a.type)
        } function r(a, g) { g = g || { x: 0, y: 0 }; if ("function" === typeof w.MouseEvent) return new w.MouseEvent(a, { bubbles: !0, cancelable: !0, composed: !0, view: w, detail: "click" === a ? 1 : 0, screenX: g.x, screenY: g.y, clientX: g.x, clientY: g.y }); if (m.createEvent) { var n = m.createEvent("MouseEvent"); if (n.initMouseEvent) return n.initMouseEvent(a, !0, !0, w, "click" === a ? 1 : 0, g.x, g.y, g.x, g.y, !1, !1, !1, !1, 0, null), n } return { type: a } } var m = a.doc, w = a.win, B = h.css; return {
            addClass: function (a, g) {
                a.classList ? a.classList.add(g) :
                0 > a.className.indexOf(g) && (a.className += " " + g)
            }, cloneMouseEvent: k, cloneTouchEvent: function (a) {
                var g = function (a) { for (var g = [], c = 0; c < a.length; ++c) { var e = a.item(c); e && g.push(e) } return g }; if ("function" === typeof w.TouchEvent) return g = new w.TouchEvent(a.type, { touches: g(a.touches), targetTouches: g(a.targetTouches), changedTouches: g(a.changedTouches), ctrlKey: a.ctrlKey, shiftKey: a.shiftKey, altKey: a.altKey, metaKey: a.metaKey, bubbles: a.bubbles, cancelable: a.cancelable, composed: a.composed, detail: a.detail, view: a.view }),
                    a.defaultPrevented && g.preventDefault(), g; g = k(a); g.touches = a.touches; g.changedTouches = a.changedTouches; g.targetTouches = a.targetTouches; return g
            }, escapeStringForHTML: function (a) { return a.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&#x27;").replace(/\//g, "&#x2F;") }, getElement: function (a) { return m.getElementById(a) }, getFakeMouseEvent: r, getHeadingTagNameForElement: function (a) {
                var g = function (a) { a = parseInt(a.slice(1), 10); return "h" + Math.min(6, a + 1) },
                n = function (a) { var c; a: { for (c = a; c = c.previousSibling;) { var e = c.tagName || ""; if (/H[1-6]/.test(e)) { c = e; break a } } c = "" } if (c) return g(c); a = a.parentElement; if (!a) return "p"; c = a.tagName; return /H[1-6]/.test(c) ? g(c) : n(a) }; return n(a)
            }, removeChildNodes: function (a) { for (; a.lastChild;)a.removeChild(a.lastChild) }, removeClass: function (a, g) { a.classList ? a.classList.remove(g) : a.className = a.className.replace(new RegExp(g, "g"), "") }, removeElement: function (a) { a && a.parentNode && a.parentNode.removeChild(a) }, reverseChildNodes: function (a) {
                for (var g =
                    a.childNodes.length; g--;)a.appendChild(a.childNodes[g])
            }, stripHTMLTagsFromString: function (a) { return "string" === typeof a ? a.replace(/<\/?[^>]+(>|$)/g, "") : a }, visuallyHideElement: function (a) { B(a, { position: "absolute", width: "1px", height: "1px", overflow: "hidden", whiteSpace: "nowrap", clip: "rect(1px, 1px, 1px, 1px)", marginTop: "-3px", "-ms-filter": "progid:DXImageTransform.Microsoft.Alpha(Opacity=1)", filter: "alpha(opacity=1)", opacity: .01 }) }
        }
    }); x(a, "Accessibility/Utils/ChartUtilities.js", [a["Core/Globals.js"], a["Accessibility/Utils/HTMLUtilities.js"],
    a["Core/Utilities.js"]], function (a, h, t) {
        function k(b, f) { var a = f.type, c = b.hcEvents; q.createEvent && (b.dispatchEvent || b.fireEvent) ? b.dispatchEvent ? b.dispatchEvent(f) : b.fireEvent(a, f) : c && c[a] ? d(b, a, f) : b.element && k(b.element, f) } function m(b) {
            var d = b.chart, a = {}, c = "Seconds"; a.Seconds = ((b.max || 0) - (b.min || 0)) / 1E3; a.Minutes = a.Seconds / 60; a.Hours = a.Minutes / 60; a.Days = a.Hours / 24;["Minutes", "Hours", "Days"].forEach(function (b) { 2 < a[b] && (c = b) }); var e = a[c].toFixed("Seconds" !== c && "Minutes" !== c ? 1 : 0); return d.langFormat("accessibility.axis.timeRange" +
                c, { chart: d, axis: b, range: e.replace(".0", "") })
        } function w(b) { var d = b.chart, a = d.options, c = a && a.accessibility && a.accessibility.screenReaderSection.axisRangeDateFormat || ""; a = function (f) { return b.dateTime ? d.time.dateFormat(c, b[f]) : b[f] }; return d.langFormat("accessibility.axis.rangeFromTo", { chart: d, axis: b, rangeFrom: a("min"), rangeTo: a("max") }) } function B(b) { if (b.points && b.points.length) return (b = e(b.points, function (b) { return !!b.graphic })) && b.graphic && b.graphic.element } function n(b) {
            var d = B(b); return d && d.parentNode ||
                b.graph && b.graph.element || b.group && b.group.element
        } function g(b, d) { d.setAttribute("aria-hidden", !1); d !== b.renderTo && d.parentNode && d.parentNode !== q.body && (Array.prototype.forEach.call(d.parentNode.childNodes, function (b) { b.hasAttribute("aria-hidden") || b.setAttribute("aria-hidden", !0) }), g(b, d.parentNode)) } var q = a.doc, y = h.stripHTMLTagsFromString, c = t.defined, e = t.find, d = t.fireEvent; return {
            fireEventOnWrappedOrUnwrappedElement: k, getChartTitle: function (b) {
                return y(b.options.title.text || b.langFormat("accessibility.defaultChartTitle",
                    { chart: b }))
            }, getAxisDescription: function (b) { return b && (b.userOptions && b.userOptions.accessibility && b.userOptions.accessibility.description || b.axisTitle && b.axisTitle.textStr || b.options.id || b.categories && "categories" || b.dateTime && "Time" || "values") }, getAxisRangeDescription: function (b) {
                var d = b.options || {}; return d.accessibility && "undefined" !== typeof d.accessibility.rangeDescription ? d.accessibility.rangeDescription : b.categories ? (d = b.chart, b = b.dataMax && b.dataMin ? d.langFormat("accessibility.axis.rangeCategories",
                    { chart: d, axis: b, numCategories: b.dataMax - b.dataMin + 1 }) : "", b) : !b.dateTime || 0 !== b.min && 0 !== b.dataMin ? w(b) : m(b)
            }, getPointFromXY: function (b, d, a) { for (var f = b.length, c; f--;)if (c = e(b[f].points || [], function (b) { return b.x === d && b.y === a })) return c }, getSeriesFirstPointElement: B, getSeriesFromName: function (b, d) { return d ? (b.series || []).filter(function (b) { return b.name === d }) : b.series }, getSeriesA11yElement: n, unhideChartElementFromAT: g, hideSeriesFromAT: function (b) { (b = n(b)) && b.setAttribute("aria-hidden", !0) }, scrollToPoint: function (b) {
                var a =
                    b.series.xAxis, e = b.series.yAxis, v = a && a.scrollbar ? a : e; if ((a = v && v.scrollbar) && c(a.to) && c(a.from)) { e = a.to - a.from; if (c(v.dataMin) && c(v.dataMax)) { var g = v.toPixels(v.dataMin), y = v.toPixels(v.dataMax); b = (v.toPixels(b["xAxis" === v.coll ? "x" : "y"] || 0) - g) / (y - g) } else b = 0; a.updatePosition(b - e / 2, b + e / 2); d(a, "changed", { from: a.from, to: a.to, trigger: "scrollbar", DOMEvent: null }) }
            }
        }
    }); x(a, "Accessibility/Utils/DOMElementProvider.js", [a["Core/Globals.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h) {
        var k = a.doc,
        r = h.removeElement; return function () { function a() { this.elements = [] } a.prototype.createElement = function () { var a = k.createElement.apply(k, arguments); this.elements.push(a); return a }; a.prototype.destroyCreatedElements = function () { this.elements.forEach(function (a) { r(a) }); this.elements = [] }; return a }()
    }); x(a, "Accessibility/Utils/EventProvider.js", [a["Core/Globals.js"], a["Core/Utilities.js"]], function (a, h) {
        var k = h.addEvent; return function () {
            function h() { this.eventRemovers = [] } h.prototype.addEvent = function () {
                var h =
                    k.apply(a, arguments); this.eventRemovers.push(h); return h
            }; h.prototype.removeAddedEvents = function () { this.eventRemovers.forEach(function (a) { return a() }); this.eventRemovers = [] }; return h
        }()
    }); x(a, "Accessibility/AccessibilityComponent.js", [a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/DOMElementProvider.js"], a["Accessibility/Utils/EventProvider.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Core/Utilities.js"]], function (a, h, t, r, m) {
        var k = a.fireEventOnWrappedOrUnwrappedElement, B = r.getFakeMouseEvent;
        a = m.extend; r = function () {
            function a() { this.proxyProvider = this.keyCodes = this.eventProvider = this.domElementProvider = this.chart = void 0 } a.prototype.initBase = function (a, q) { this.chart = a; this.eventProvider = new t; this.domElementProvider = new h; this.proxyProvider = q; this.keyCodes = { left: 37, right: 39, up: 38, down: 40, enter: 13, space: 32, esc: 27, tab: 9, pageUp: 33, pageDown: 34, end: 35, home: 36 } }; a.prototype.addEvent = function (a, q, y, c) { return this.eventProvider.addEvent(a, q, y, c) }; a.prototype.createElement = function (a, q) {
                return this.domElementProvider.createElement(a,
                    q)
            }; a.prototype.fakeClickEvent = function (a) { var g = B("click"); k(a, g) }; a.prototype.destroyBase = function () { this.domElementProvider.destroyCreatedElements(); this.eventProvider.removeAddedEvents() }; return a
        }(); a(r.prototype, { init: function () { }, getKeyboardNavigation: function () { }, onChartUpdate: function () { }, onChartRender: function () { }, destroy: function () { } }); return r
    }); x(a, "Accessibility/KeyboardNavigationHandler.js", [a["Core/Utilities.js"]], function (a) {
        var h = a.find; a = function () {
            function a(a, h) {
                this.chart = a;
                this.keyCodeMap = h.keyCodeMap || []; this.validate = h.validate; this.init = h.init; this.terminate = h.terminate; this.response = { success: 1, prev: 2, next: 3, noHandler: 4, fail: 5 }
            } a.prototype.run = function (a) { var k = a.which || a.keyCode, w = this.response.noHandler, t = h(this.keyCodeMap, function (a) { return -1 < a[0].indexOf(k) }); t ? w = t[1].call(this, k, a) : 9 === k && (w = this.response[a.shiftKey ? "prev" : "next"]); return w }; return a
        }(); ""; return a
    }); x(a, "Accessibility/Components/ContainerComponent.js", [a["Accessibility/AccessibilityComponent.js"],
    a["Accessibility/KeyboardNavigationHandler.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Core/Globals.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h, t, r, m) {
        var k = this && this.__extends || function () {
            var a = function (c, e) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b } || function (d, b) { for (var a in b) b.hasOwnProperty(a) && (d[a] = b[a]) }; return a(c, e) }; return function (c, e) {
                function d() { this.constructor = c } a(c, e); c.prototype = null === e ? Object.create(e) : (d.prototype =
                    e.prototype, new d)
            }
        }(), B = t.unhideChartElementFromAT, n = t.getChartTitle, g = r.doc, q = m.stripHTMLTagsFromString; return function (a) {
            function c() { return null !== a && a.apply(this, arguments) || this } k(c, a); c.prototype.onChartUpdate = function () { this.handleSVGTitleElement(); this.setSVGContainerLabel(); this.setGraphicContainerAttrs(); this.setRenderToAttrs(); this.makeCreditsAccessible() }; c.prototype.handleSVGTitleElement = function () {
                var a = this.chart, d = "highcharts-title-" + a.index, b = q(a.langFormat("accessibility.svgContainerTitle",
                    { chartTitle: n(a) })); if (b.length) { var c = this.svgTitleElement = this.svgTitleElement || g.createElementNS("http://www.w3.org/2000/svg", "title"); c.textContent = b; c.id = d; a.renderTo.insertBefore(c, a.renderTo.firstChild) }
            }; c.prototype.setSVGContainerLabel = function () { var a = this.chart, d = a.langFormat("accessibility.svgContainerLabel", { chartTitle: n(a) }); a.renderer.box && d.length && a.renderer.box.setAttribute("aria-label", d) }; c.prototype.setGraphicContainerAttrs = function () {
                var a = this.chart, d = a.langFormat("accessibility.graphicContainerLabel",
                    { chartTitle: n(a) }); d.length && a.container.setAttribute("aria-label", d)
            }; c.prototype.setRenderToAttrs = function () { var a = this.chart; "disabled" !== a.options.accessibility.landmarkVerbosity ? a.renderTo.setAttribute("role", "region") : a.renderTo.removeAttribute("role"); a.renderTo.setAttribute("aria-label", a.langFormat("accessibility.chartContainerLabel", { title: n(a), chart: a })) }; c.prototype.makeCreditsAccessible = function () {
                var a = this.chart, d = a.credits; d && (d.textStr && d.element.setAttribute("aria-label", a.langFormat("accessibility.credits",
                    { creditsStr: q(d.textStr) })), B(a, d.element))
            }; c.prototype.getKeyboardNavigation = function () { var a = this.chart; return new h(a, { keyCodeMap: [], validate: function () { return !0 }, init: function () { var d = a.accessibility; d && d.keyboardNavigation.tabindexContainer.focus() } }) }; c.prototype.destroy = function () { this.chart.renderTo.setAttribute("aria-hidden", !0) }; return c
        }(a)
    }); x(a, "Accessibility/FocusBorder.js", [a["Core/Renderer/SVG/SVGLabel.js"], a["Core/Utilities.js"]], function (a, h) {
        var k = h.addEvent, r = h.pick, m; (function (h) {
            function m() {
                var a =
                    this.focusElement, b = this.options.accessibility.keyboardNavigation.focusBorder; a && (a.removeFocusBorder(), b.enabled && a.addFocusBorder(b.margin, { stroke: b.style.color, strokeWidth: b.style.lineWidth, r: b.style.borderRadius }))
            } function n(a, b) {
                var d = this.options.accessibility.keyboardNavigation.focusBorder; (b = b || a.element) && b.focus && (b.hcEvents && b.hcEvents.focusin || k(b, "focusin", function () { }), b.focus(), d.hideBrowserFocusOutline && (b.style.outline = "none")); this.focusElement && this.focusElement.removeFocusBorder();
                this.focusElement = a; this.renderFocusBorder()
            } function g(a) { if (!a.focusBorderDestroyHook) { var b = a.destroy; a.destroy = function () { a.focusBorder && a.focusBorder.destroy && a.focusBorder.destroy(); return b.apply(a, arguments) }; a.focusBorderDestroyHook = b } } function q(b, d) {
                this.focusBorder && this.removeFocusBorder(); var c = this.getBBox(), f = r(b, 3); c.x += this.translateX ? this.translateX : 0; c.y += this.translateY ? this.translateY : 0; var e = c.x - f, u = c.y - f, z = c.width + 2 * f, D = c.height + 2 * f, q = this instanceof a; if ("text" === this.element.nodeName ||
                    q) { var h = !!this.rotation; if (q) var n = { x: h ? 1 : 0, y: 0 }; else { var k = n = 0; "middle" === this.attr("text-anchor") ? n = k = .5 : this.rotation ? n = .25 : k = .75; n = { x: n, y: k } } k = +this.attr("x"); var l = +this.attr("y"); isNaN(k) || (e = k - c.width * n.x - f); isNaN(l) || (u = l - c.height * n.y - f); q && h && (q = z, z = D, D = q, isNaN(k) || (e = k - c.height * n.x - f), isNaN(l) || (u = l - c.width * n.y - f)) } this.focusBorder = this.renderer.rect(e, u, z, D, parseInt((d && d.r || 0).toString(), 10)).addClass("highcharts-focus-border").attr({ zIndex: 99 }).add(this.parentGroup); this.renderer.styledMode ||
                        this.focusBorder.attr({ stroke: d && d.stroke, "stroke-width": d && d.strokeWidth }); y(this, b, d); g(this)
            } function y(a) { for (var d = [], c = 1; c < arguments.length; c++)d[c - 1] = arguments[c]; a.focusBorderUpdateHooks || (a.focusBorderUpdateHooks = {}, b.forEach(function (b) { b += "Setter"; var c = a[b] || a._defaultSetter; a.focusBorderUpdateHooks[b] = c; a[b] = function () { var b = c.apply(a, arguments); a.addFocusBorder.apply(a, d); return b } })) } function c() {
                e(this); this.focusBorderDestroyHook && (this.destroy = this.focusBorderDestroyHook, delete this.focusBorderDestroyHook);
                this.focusBorder && (this.focusBorder.destroy(), delete this.focusBorder)
            } function e(a) { a.focusBorderUpdateHooks && (Object.keys(a.focusBorderUpdateHooks).forEach(function (b) { var d = a.focusBorderUpdateHooks[b]; d === a._defaultSetter ? delete a[b] : a[b] = d }), delete a.focusBorderUpdateHooks) } var d = [], b = "x y transform width height r d stroke-width".split(" "); h.compose = function (a, b) {
                -1 === d.indexOf(a) && (d.push(a), a = a.prototype, a.renderFocusBorder = m, a.setFocusToElement = n); -1 === d.indexOf(b) && (d.push(b), b = b.prototype,
                    b.addFocusBorder = q, b.removeFocusBorder = c)
            }
        })(m || (m = {})); return m
    }); x(a, "Accessibility/Utils/Announcer.js", [a["Core/Renderer/HTML/AST.js"], a["Accessibility/Utils/DOMElementProvider.js"], a["Core/Globals.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Core/Utilities.js"]], function (a, h, t, r, m) {
        var k = t.doc, B = r.addClass, n = r.visuallyHideElement, g = m.attr; return function () {
            function q(a, c) { this.chart = a; this.domElementProvider = new h; this.announceRegion = this.addAnnounceRegion(c) } q.prototype.destroy = function () { this.domElementProvider.destroyCreatedElements() };
            q.prototype.announce = function (g) { var c = this; a.setElementHTML(this.announceRegion, g); this.clearAnnouncementRegionTimer && clearTimeout(this.clearAnnouncementRegionTimer); this.clearAnnouncementRegionTimer = setTimeout(function () { c.announceRegion.innerHTML = a.emptyHTML; delete c.clearAnnouncementRegionTimer }, 1E3) }; q.prototype.addAnnounceRegion = function (a) {
                var c = this.chart.announcerContainer || this.createAnnouncerContainer(), e = this.domElementProvider.createElement("div"); g(e, { "aria-hidden": !1, "aria-live": a });
                this.chart.styledMode ? B(e, "highcharts-visually-hidden") : n(e); c.appendChild(e); return e
            }; q.prototype.createAnnouncerContainer = function () { var a = this.chart, c = k.createElement("div"); g(c, { "aria-hidden": !1, "class": "highcharts-announcer-container" }); c.style.position = "relative"; a.renderTo.insertBefore(c, a.renderTo.firstChild); return a.announcerContainer = c }; return q
        }()
    }); x(a, "Accessibility/Components/AnnotationsA11y.js", [a["Accessibility/Utils/HTMLUtilities.js"]], function (a) {
        function h(a) {
            return (a.annotations ||
                []).reduce(function (a, q) { q.options && !1 !== q.options.visible && (a = a.concat(q.labels)); return a }, [])
        } function k(a) { return a.options && a.options.accessibility && a.options.accessibility.description || a.graphic && a.graphic.text && a.graphic.text.textStr || "" } function r(a) {
            var g = a.options && a.options.accessibility && a.options.accessibility.description; if (g) return g; g = a.chart; var q = k(a), h = a.points.filter(function (a) { return !!a.graphic }).map(function (a) {
                var b = a.accessibility && a.accessibility.valueDescription || a.graphic &&
                    a.graphic.element && a.graphic.element.getAttribute("aria-label") || ""; a = a && a.series.name || ""; return (a ? a + ", " : "") + "data point " + b
            }).filter(function (a) { return !!a }), c = h.length, e = "accessibility.screenReaderSection.annotations.description" + (1 < c ? "MultiplePoints" : c ? "SinglePoint" : "NoPoints"); a = { annotationText: q, annotation: a, numPoints: c, annotationPoint: h[0], additionalAnnotationPoints: h.slice(1) }; return g.langFormat(e, a)
        } function m(a) { return h(a).map(function (a) { return (a = w(B(r(a)))) ? "<li>" + a + "</li>" : "" }) } var w =
            a.escapeStringForHTML, B = a.stripHTMLTagsFromString; return { getAnnotationsInfoHTML: function (a) { var g = a.annotations; return g && g.length ? '<ul style="list-style-type: none">' + m(a).join(" ") + "</ul>" : "" }, getAnnotationLabelDescription: r, getAnnotationListItems: m, getPointAnnotationTexts: function (a) { var g = h(a.series.chart).filter(function (g) { return -1 < g.points.indexOf(a) }); return g.length ? g.map(function (a) { return "" + k(a) }) : [] } }
    }); x(a, "Accessibility/Components/InfoRegionsComponent.js", [a["Accessibility/A11yI18n.js"],
    a["Accessibility/AccessibilityComponent.js"], a["Accessibility/Utils/Announcer.js"], a["Accessibility/Components/AnnotationsA11y.js"], a["Core/Renderer/HTML/AST.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Core/FormatUtilities.js"], a["Core/Globals.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Core/Utilities.js"]], function (a, h, t, r, m, w, B, n, g, q) {
        function y(a, b) {
            var d = b[0], c = a.series && a.series[0] || {}; c = { numSeries: a.series.length, numPoints: c.points && c.points.length, chart: a, mapTitle: c.mapTitle };
            if (!d) return a.langFormat("accessibility.chartTypes.emptyChart", c); if ("map" === d) return c.mapTitle ? a.langFormat("accessibility.chartTypes.mapTypeDescription", c) : a.langFormat("accessibility.chartTypes.unknownMap", c); if (1 < a.types.length) return a.langFormat("accessibility.chartTypes.combinationChart", c); b = b[0]; d = a.langFormat("accessibility.seriesTypeDescriptions." + b, c); var e = a.series && 2 > a.series.length ? "Single" : "Multiple"; return (a.langFormat("accessibility.chartTypes." + b + e, c) || a.langFormat("accessibility.chartTypes.default" +
                e, c)) + (d ? " " + d : "")
        } var c = this && this.__extends || function () { var a = function (b, d) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, b) { a.__proto__ = b } || function (a, b) { for (var d in b) b.hasOwnProperty(d) && (a[d] = b[d]) }; return a(b, d) }; return function (b, d) { function c() { this.constructor = b } a(b, d); b.prototype = null === d ? Object.create(d) : (c.prototype = d.prototype, new c) } }(), e = r.getAnnotationsInfoHTML, d = w.getAxisDescription, b = w.getAxisRangeDescription, f = w.getChartTitle, u = w.unhideChartElementFromAT,
            v = B.format, k = n.doc, E = g.addClass, G = g.getElement, z = g.getHeadingTagNameForElement, D = g.stripHTMLTagsFromString, F = g.visuallyHideElement, C = q.attr, H = q.pick; return function (g) {
                function l() { var a = null !== g && g.apply(this, arguments) || this; a.announcer = void 0; a.screenReaderSections = {}; return a } c(l, g); l.prototype.init = function () {
                    var a = this.chart, b = this; this.initRegionsDefinitions(); this.addEvent(a, "aftergetTableAST", function (a) { b.onDataTableCreated(a) }); this.addEvent(a, "afterViewData", function (a) {
                        b.dataTableDiv =
                        a; setTimeout(function () { b.focusDataTable() }, 300)
                    }); this.announcer = new t(a, "assertive")
                }; l.prototype.initRegionsDefinitions = function () {
                    var a = this; this.screenReaderSections = {
                        before: {
                            element: null, buildContent: function (b) { var d = b.options.accessibility.screenReaderSection.beforeChartFormatter; return d ? d(b) : a.defaultBeforeChartFormatter(b) }, insertIntoDOM: function (a, b) { b.renderTo.insertBefore(a, b.renderTo.firstChild) }, afterInserted: function () {
                                "undefined" !== typeof a.sonifyButtonId && a.initSonifyButton(a.sonifyButtonId);
                                "undefined" !== typeof a.dataTableButtonId && a.initDataTableButton(a.dataTableButtonId)
                            }
                        }, after: { element: null, buildContent: function (b) { var d = b.options.accessibility.screenReaderSection.afterChartFormatter; return d ? d(b) : a.defaultAfterChartFormatter() }, insertIntoDOM: function (a, b) { b.renderTo.insertBefore(a, b.container.nextSibling) }, afterInserted: function () { a.chart.accessibility && a.chart.accessibility.keyboardNavigation.updateExitAnchor() } }
                    }
                }; l.prototype.onChartRender = function () {
                    var a = this; this.linkedDescriptionElement =
                        this.getLinkedDescriptionElement(); this.setLinkedDescriptionAttrs(); Object.keys(this.screenReaderSections).forEach(function (b) { a.updateScreenReaderSection(b) })
                }; l.prototype.getLinkedDescriptionElement = function () { var a = this.chart.options.accessibility.linkedDescription; if (a) { if ("string" !== typeof a) return a; a = v(a, this.chart); a = k.querySelectorAll(a); if (1 === a.length) return a[0] } }; l.prototype.setLinkedDescriptionAttrs = function () {
                    var a = this.linkedDescriptionElement; a && (a.setAttribute("aria-hidden", "true"),
                        E(a, "highcharts-linked-description"))
                }; l.prototype.updateScreenReaderSection = function (a) { var b = this.chart, d = this.screenReaderSections[a], c = d.buildContent(b), p = d.element = d.element || this.createElement("div"), e = p.firstChild || this.createElement("div"); c ? (this.setScreenReaderSectionAttribs(p, a), m.setElementHTML(e, c), p.appendChild(e), d.insertIntoDOM(p, b), b.styledMode ? E(e, "highcharts-visually-hidden") : F(e), u(b, e), d.afterInserted && d.afterInserted()) : (p.parentNode && p.parentNode.removeChild(p), delete d.element) };
                l.prototype.setScreenReaderSectionAttribs = function (a, b) { var d = this.chart, c = d.langFormat("accessibility.screenReaderSection." + b + "RegionLabel", { chart: d, chartTitle: f(d) }); C(a, { id: "highcharts-screen-reader-region-" + b + "-" + d.index, "aria-label": c }); a.style.position = "relative"; "all" === d.options.accessibility.landmarkVerbosity && c && a.setAttribute("role", "region") }; l.prototype.defaultBeforeChartFormatter = function () {
                    var b = this.chart, d = b.options.accessibility.screenReaderSection.beforeChartFormat; if (!d) return "";
                    var c = this.getAxesDescription(), M = b.sonify && b.options.sonification && b.options.sonification.enabled, I = "highcharts-a11y-sonify-data-btn-" + b.index, l = "hc-linkto-highcharts-data-table-" + b.index, u = e(b), v = b.langFormat("accessibility.screenReaderSection.annotations.heading", { chart: b }); c = {
                        headingTagName: z(b.renderTo), chartTitle: f(b), typeDescription: this.getTypeDescriptionText(), chartSubtitle: this.getSubtitleText(), chartLongdesc: this.getLongdescText(), xAxisDescription: c.xAxis, yAxisDescription: c.yAxis, playAsSoundButton: M ?
                            this.getSonifyButtonText(I) : "", viewTableButton: b.getCSV ? this.getDataTableButtonText(l) : "", annotationsTitle: u ? v : "", annotationsList: u
                    }; b = a.i18nFormat(d, c, b); this.dataTableButtonId = l; this.sonifyButtonId = I; return b.replace(/<(\w+)[^>]*?>\s*<\/\1>/g, "")
                }; l.prototype.defaultAfterChartFormatter = function () {
                    var b = this.chart, d = b.options.accessibility.screenReaderSection.afterChartFormat; if (!d) return ""; var c = { endOfChartMarker: this.getEndOfChartMarkerText() }; return a.i18nFormat(d, c, b).replace(/<(\w+)[^>]*?>\s*<\/\1>/g,
                        "")
                }; l.prototype.getLinkedDescription = function () { var a = this.linkedDescriptionElement; return D(a && a.innerHTML || "") }; l.prototype.getLongdescText = function () { var a = this.chart.options, b = a.caption; b = b && b.text; var d = this.getLinkedDescription(); return a.accessibility.description || d || b || "" }; l.prototype.getTypeDescriptionText = function () { var a = this.chart; return a.types ? a.options.accessibility.typeDescription || y(a, a.types) : "" }; l.prototype.getDataTableButtonText = function (a) {
                    var b = this.chart; b = b.langFormat("accessibility.table.viewAsDataTableButtonText",
                        { chart: b, chartTitle: f(b) }); return '<button id="' + a + '">' + b + "</button>"
                }; l.prototype.getSonifyButtonText = function (a) { var b = this.chart; if (b.options.sonification && !1 === b.options.sonification.enabled) return ""; b = b.langFormat("accessibility.sonification.playAsSoundButtonText", { chart: b, chartTitle: f(b) }); return '<button id="' + a + '">' + b + "</button>" }; l.prototype.getSubtitleText = function () { var a = this.chart.options.subtitle; return D(a && a.text || "") }; l.prototype.getEndOfChartMarkerText = function () {
                    var a = this.chart,
                    b = a.langFormat("accessibility.screenReaderSection.endOfChartMarker", { chart: a }); return '<div id="highcharts-end-of-chart-marker-' + a.index + '">' + b + "</div>"
                }; l.prototype.onDataTableCreated = function (a) { var b = this.chart; if (b.options.accessibility.enabled) { this.viewDataTableButton && this.viewDataTableButton.setAttribute("aria-expanded", "true"); var d = a.tree.attributes || {}; d.tabindex = -1; d.summary = b.langFormat("accessibility.table.tableSummary", { chart: b }); a.tree.attributes = d } }; l.prototype.focusDataTable = function () {
                    var a =
                        this.dataTableDiv; (a = a && a.getElementsByTagName("table")[0]) && a.focus && a.focus()
                }; l.prototype.initSonifyButton = function (a) {
                    var b = this, d = this.sonifyButton = G(a), c = this.chart, e = function (a) {
                        d && (d.setAttribute("aria-hidden", "true"), d.setAttribute("aria-label", "")); a.preventDefault(); a.stopPropagation(); a = c.langFormat("accessibility.sonification.playAsSoundClickAnnouncement", { chart: c }); b.announcer.announce(a); setTimeout(function () {
                            d && (d.removeAttribute("aria-hidden"), d.removeAttribute("aria-label")); c.sonify &&
                                c.sonify()
                        }, 1E3)
                    }; d && c && (d.setAttribute("tabindex", -1), d.onclick = function (a) { (c.options.accessibility && c.options.accessibility.screenReaderSection.onPlayAsSoundClick || e).call(this, a, c) })
                }; l.prototype.initDataTableButton = function (a) { var b = this.viewDataTableButton = G(a), d = this.chart; a = a.replace("hc-linkto-", ""); b && (C(b, { tabindex: -1, "aria-expanded": !!G(a) }), b.onclick = d.options.accessibility.screenReaderSection.onViewDataTableClick || function () { d.viewData() }) }; l.prototype.getAxesDescription = function () {
                    var a =
                        this.chart, b = function (b, d) { b = a[b]; return 1 < b.length || b[0] && H(b[0].options.accessibility && b[0].options.accessibility.enabled, d) }, d = !!a.types && 0 > a.types.indexOf("map"), c = !!a.hasCartesianSeries, e = b("xAxis", !a.angular && c && d); b = b("yAxis", c && d); d = {}; e && (d.xAxis = this.getAxisDescriptionText("xAxis")); b && (d.yAxis = this.getAxisDescriptionText("yAxis")); return d
                }; l.prototype.getAxisDescriptionText = function (a) {
                    var c = this.chart, e = c[a]; return c.langFormat("accessibility.axis." + a + "Description" + (1 < e.length ? "Plural" :
                        "Singular"), { chart: c, names: e.map(function (a) { return d(a) }), ranges: e.map(function (a) { return b(a) }), numAxes: e.length })
                }; l.prototype.destroy = function () { this.announcer && this.announcer.destroy() }; return l
            }(h)
    }); x(a, "Accessibility/Components/MenuComponent.js", [a["Core/Chart/Chart.js"], a["Core/Utilities.js"], a["Accessibility/AccessibilityComponent.js"], a["Accessibility/KeyboardNavigationHandler.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h, t, r, m,
        w) {
            var k = this && this.__extends || function () { var a = function (c, d) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, d) { a.__proto__ = d } || function (a, d) { for (var b in d) d.hasOwnProperty(b) && (a[b] = d[b]) }; return a(c, d) }; return function (c, d) { function b() { this.constructor = c } a(c, d); c.prototype = null === d ? Object.create(d) : (b.prototype = d.prototype, new b) } }(), n = h.attr, g = m.getChartTitle, q = m.unhideChartElementFromAT, y = w.getFakeMouseEvent; h = function (a) {
                function c() {
                    return null !== a && a.apply(this, arguments) ||
                        this
                } k(c, a); c.prototype.init = function () { var a = this.chart, b = this; this.addEvent(a, "exportMenuShown", function () { b.onMenuShown() }); this.addEvent(a, "exportMenuHidden", function () { b.onMenuHidden() }); this.createProxyGroup() }; c.prototype.onMenuHidden = function () { var a = this.chart.exportContextMenu; a && a.setAttribute("aria-hidden", "true"); this.isExportMenuShown = !1; this.setExportButtonExpandedState("false") }; c.prototype.onMenuShown = function () {
                    var a = this.chart, b = a.exportContextMenu; b && (this.addAccessibleContextMenuAttribs(),
                        q(a, b)); this.isExportMenuShown = !0; this.setExportButtonExpandedState("true")
                }; c.prototype.setExportButtonExpandedState = function (a) { this.exportButtonProxy && this.exportButtonProxy.buttonElement.setAttribute("aria-expanded", a) }; c.prototype.onChartRender = function () { this.proxyProvider.clearGroup("chartMenu"); this.proxyMenuButton() }; c.prototype.proxyMenuButton = function () {
                    var a = this.chart, b = this.proxyProvider, c = a.exportSVGElements && a.exportSVGElements[0], e = a.options.exporting, v = a.exportSVGElements && a.exportSVGElements[0];
                    e && !1 !== e.enabled && e.accessibility && e.accessibility.enabled && v && v.element && c && (this.exportButtonProxy = b.addProxyElement("chartMenu", { click: c }, { "aria-label": a.langFormat("accessibility.exporting.menuButtonLabel", { chart: a, chartTitle: g(a) }), "aria-expanded": !1 }))
                }; c.prototype.createProxyGroup = function () { this.chart && this.proxyProvider && this.proxyProvider.addGroup("chartMenu", "div") }; c.prototype.addAccessibleContextMenuAttribs = function () {
                    var a = this.chart, b = a.exportDivElements; b && b.length && (b.forEach(function (a) {
                        a &&
                        ("LI" !== a.tagName || a.children && a.children.length ? a.setAttribute("aria-hidden", "true") : a.setAttribute("tabindex", -1))
                    }), (b = b[0] && b[0].parentNode) && n(b, { "aria-hidden": void 0, "aria-label": a.langFormat("accessibility.exporting.chartMenuLabel", { chart: a }), role: "list" }))
                }; c.prototype.getKeyboardNavigation = function () {
                    var a = this.keyCodes, b = this.chart, c = this; return new r(b, {
                        keyCodeMap: [[[a.left, a.up], function () { return c.onKbdPrevious(this) }], [[a.right, a.down], function () { return c.onKbdNext(this) }], [[a.enter,
                        a.space], function () { return c.onKbdClick(this) }]], validate: function () { return !!b.exporting && !1 !== b.options.exporting.enabled && !1 !== b.options.exporting.accessibility.enabled }, init: function () { var a = c.exportButtonProxy, d = c.chart.exportingGroup; a && d && b.setFocusToElement(d, a.buttonElement) }, terminate: function () { b.hideExportMenu() }
                    })
                }; c.prototype.onKbdPrevious = function (a) {
                    var b = this.chart, d = b.options.accessibility; a = a.response; for (var c = b.highlightedExportItemIx || 0; c--;)if (b.highlightExportItem(c)) return a.success;
                    return d.keyboardNavigation.wrapAround ? (b.highlightLastExportItem(), a.success) : a.prev
                }; c.prototype.onKbdNext = function (a) { var b = this.chart, d = b.options.accessibility; a = a.response; for (var c = (b.highlightedExportItemIx || 0) + 1; c < b.exportDivElements.length; ++c)if (b.highlightExportItem(c)) return a.success; return d.keyboardNavigation.wrapAround ? (b.highlightExportItem(0), a.success) : a.next }; c.prototype.onKbdClick = function (a) {
                    var b = this.chart, d = b.exportDivElements[b.highlightedExportItemIx], c = (b.exportSVGElements &&
                        b.exportSVGElements[0]).element; this.isExportMenuShown ? this.fakeClickEvent(d) : (this.fakeClickEvent(c), b.highlightExportItem(0)); return a.response.success
                }; return c
            }(t); (function (c) {
                function e() { var a = this.exportSVGElements && this.exportSVGElements[0]; if (a && (a = a.element, a.onclick)) a.onclick(y("click")) } function d() {
                    var a = this.exportDivElements; a && this.exportContextMenu && (a.forEach(function (a) { if (a && "highcharts-menu-item" === a.className && a.onmouseout) a.onmouseout(y("mouseout")) }), this.highlightedExportItemIx =
                        0, this.exportContextMenu.hideMenu(), this.container.focus())
                } function b(a) { var b = this.exportDivElements && this.exportDivElements[a], d = this.exportDivElements && this.exportDivElements[this.highlightedExportItemIx]; if (b && "LI" === b.tagName && (!b.children || !b.children.length)) { var c = !!(this.renderTo.getElementsByTagName("g")[0] || {}).focus; b.focus && c && b.focus(); if (d && d.onmouseout) d.onmouseout(y("mouseout")); if (b.onmouseover) b.onmouseover(y("mouseover")); this.highlightedExportItemIx = a; return !0 } return !1 } function f() {
                    if (this.exportDivElements) for (var a =
                        this.exportDivElements.length; a--;)if (this.highlightExportItem(a)) return !0; return !1
                } var u = []; c.compose = function (c) { -1 === u.indexOf(c) && (u.push(c), c = a.prototype, c.hideExportMenu = d, c.highlightExportItem = b, c.highlightLastExportItem = f, c.showExportMenu = e) }
            })(h || (h = {})); return h
    }); x(a, "Accessibility/KeyboardNavigation.js", [a["Core/Globals.js"], a["Accessibility/Components/MenuComponent.js"], a["Core/Utilities.js"], a["Accessibility/Utils/EventProvider.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a,
        h, t, r, m) {
            var k = a.doc, B = a.win, n = t.addEvent, g = t.fireEvent, q = m.getElement; t = function () {
                function a(a, e) { this.components = this.chart = void 0; this.currentModuleIx = NaN; this.exitAnchor = this.eventProvider = void 0; this.modules = []; this.tabindexContainer = void 0; this.init(a, e) } a.prototype.init = function (a, e) {
                    var d = this, b = this.eventProvider = new r; this.chart = a; this.components = e; this.modules = []; this.currentModuleIx = 0; this.update(); b.addEvent(this.tabindexContainer, "keydown", function (a) { return d.onKeydown(a) }); b.addEvent(this.tabindexContainer,
                        "focus", function (a) { return d.onFocus(a) });["mouseup", "touchend"].forEach(function (a) { return b.addEvent(k, a, function () { return d.onMouseUp() }) });["mousedown", "touchstart"].forEach(function (c) { return b.addEvent(a.renderTo, c, function () { d.isClickingChart = !0 }) }); b.addEvent(a.renderTo, "mouseover", function () { d.pointerIsOverChart = !0 }); b.addEvent(a.renderTo, "mouseout", function () { d.pointerIsOverChart = !1 })
                }; a.prototype.update = function (a) {
                    var c = this.chart.options.accessibility; c = c && c.keyboardNavigation; var d =
                        this.components; this.updateContainerTabindex(); c && c.enabled && a && a.length ? (this.modules = a.reduce(function (a, c) { c = d[c].getKeyboardNavigation(); return a.concat(c) }, []), this.updateExitAnchor()) : (this.modules = [], this.currentModuleIx = 0, this.removeExitAnchor())
                }; a.prototype.onFocus = function (a) { var c = this.chart; a = a.relatedTarget && c.container.contains(a.relatedTarget); this.exiting || this.tabbingInBackwards || this.isClickingChart || a || !this.modules[0] || this.modules[0].init(1); this.exiting = !1 }; a.prototype.onMouseUp =
                    function () { delete this.isClickingChart; if (!this.keyboardReset && !this.pointerIsOverChart) { var a = this.chart, e = this.modules && this.modules[this.currentModuleIx || 0]; e && e.terminate && e.terminate(); a.focusElement && a.focusElement.removeFocusBorder(); this.currentModuleIx = 0; this.keyboardReset = !0 } }; a.prototype.onKeydown = function (a) {
                        a = a || B.event; var c = this.modules && this.modules.length && this.modules[this.currentModuleIx], d; this.exiting = this.keyboardReset = !1; if (c) {
                            var b = c.run(a); b === c.response.success ? d = !0 : b ===
                                c.response.prev ? d = this.prev() : b === c.response.next && (d = this.next()); d && (a.preventDefault(), a.stopPropagation())
                        }
                    }; a.prototype.prev = function () { return this.move(-1) }; a.prototype.next = function () { return this.move(1) }; a.prototype.move = function (a) {
                        var c = this.modules && this.modules[this.currentModuleIx]; c && c.terminate && c.terminate(a); this.chart.focusElement && this.chart.focusElement.removeFocusBorder(); this.currentModuleIx += a; if (c = this.modules && this.modules[this.currentModuleIx]) {
                            if (c.validate && !c.validate()) return this.move(a);
                            if (c.init) return c.init(a), !0
                        } this.currentModuleIx = 0; this.exiting = !0; 0 < a ? this.exitAnchor.focus() : this.tabindexContainer.focus(); return !1
                    }; a.prototype.updateExitAnchor = function () { var a = q("highcharts-end-of-chart-marker-" + this.chart.index); this.removeExitAnchor(); a ? (this.makeElementAnExitAnchor(a), this.exitAnchor = a) : this.createExitAnchor() }; a.prototype.updateContainerTabindex = function () {
                        var a = this.chart.options.accessibility; a = a && a.keyboardNavigation; a = !(a && !1 === a.enabled); var e = this.chart, d = e.container;
                        e.renderTo.hasAttribute("tabindex") && (d.removeAttribute("tabindex"), d = e.renderTo); this.tabindexContainer = d; var b = d.getAttribute("tabindex"); a && !b ? d.setAttribute("tabindex", "0") : a || e.container.removeAttribute("tabindex")
                    }; a.prototype.makeElementAnExitAnchor = function (a) { var c = this.tabindexContainer.getAttribute("tabindex") || 0; a.setAttribute("class", "highcharts-exit-anchor"); a.setAttribute("tabindex", c); a.setAttribute("aria-hidden", !1); this.addExitAnchorEventsToEl(a) }; a.prototype.createExitAnchor = function () {
                        var a =
                            this.chart, e = this.exitAnchor = k.createElement("div"); a.renderTo.appendChild(e); this.makeElementAnExitAnchor(e)
                    }; a.prototype.removeExitAnchor = function () { this.exitAnchor && this.exitAnchor.parentNode && (this.exitAnchor.parentNode.removeChild(this.exitAnchor), delete this.exitAnchor) }; a.prototype.addExitAnchorEventsToEl = function (a) {
                        var c = this.chart, d = this; this.eventProvider.addEvent(a, "focus", function (a) {
                            a = a || B.event; a.relatedTarget && c.container.contains(a.relatedTarget) || d.exiting ? d.exiting = !1 : (d.tabbingInBackwards =
                                !0, d.tabindexContainer.focus(), delete d.tabbingInBackwards, a.preventDefault(), d.modules && d.modules.length && (d.currentModuleIx = d.modules.length - 1, (a = d.modules[d.currentModuleIx]) && a.validate && !a.validate() ? d.prev() : a && a.init(-1)))
                        })
                    }; a.prototype.destroy = function () { this.removeExitAnchor(); this.eventProvider.removeAddedEvents(); this.chart.container.removeAttribute("tabindex") }; return a
            }(); (function (q) {
                function c() { var a = this; g(this, "dismissPopupContent", {}, function () { a.tooltip && a.tooltip.hide(0); a.hideExportMenu() }) }
                function e(b) { 27 === (b.which || b.keyCode) && a.charts && a.charts.forEach(function (a) { a && a.dismissPopupContent && a.dismissPopupContent() }) } var d = []; q.compose = function (a) { h.compose(a); -1 === d.indexOf(a) && (d.push(a), a.prototype.dismissPopupContent = c); -1 === d.indexOf(k) && (d.push(k), n(k, "keydown", e)); return a }
            })(t || (t = {})); return t
    }); x(a, "Accessibility/Components/LegendComponent.js", [a["Core/Animation/AnimationUtilities.js"], a["Core/Globals.js"], a["Core/Legend/Legend.js"], a["Core/Utilities.js"], a["Accessibility/AccessibilityComponent.js"],
    a["Accessibility/KeyboardNavigationHandler.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h, t, r, m, w, B, n) {
        function g(a) { var b = a.legend && a.legend.allItems, d = a.options.legend.accessibility || {}; return !(!b || !b.length || a.colorAxis && a.colorAxis.length || !1 === d.enabled) } var q = this && this.__extends || function () {
            var a = function (b, d) {
                a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, b) { a.__proto__ = b } || function (a, b) {
                    for (var d in b) b.hasOwnProperty(d) &&
                        (a[d] = b[d])
                }; return a(b, d)
            }; return function (b, d) { function c() { this.constructor = b } a(b, d); b.prototype = null === d ? Object.create(d) : (c.prototype = d.prototype, new c) }
        }(), k = a.animObject, c = r.addEvent, e = r.fireEvent, d = r.isNumber, b = r.pick, f = r.syncTimeout, u = B.getChartTitle, v = n.stripHTMLTagsFromString, K = n.addClass, E = n.removeClass; a = function (a) {
            function d() { var b = null !== a && a.apply(this, arguments) || this; b.highlightedLegendItemIx = NaN; return b } q(d, a); d.prototype.init = function () {
                var a = this; this.recreateProxies(); this.addEvent(t,
                    "afterScroll", function () { this.chart === a.chart && (a.proxyProvider.updateGroupProxyElementPositions("legend"), a.updateLegendItemProxyVisibility(), -1 < a.highlightedLegendItemIx && this.chart.highlightLegendItem(a.highlightedLegendItemIx)) }); this.addEvent(t, "afterPositionItem", function (b) { this.chart === a.chart && this.chart.renderer && a.updateProxyPositionForItem(b.item) }); this.addEvent(t, "afterRender", function () {
                        this.chart === a.chart && this.chart.renderer && a.recreateProxies() && f(function () { return a.proxyProvider.updateGroupProxyElementPositions("legend") },
                            k(b(this.chart.renderer.globalAnimation, !0)).duration)
                    })
            }; d.prototype.updateLegendItemProxyVisibility = function () {
                var a = this.chart, b = a.legend, d = b.currentPage || 1, c = b.clipHeight || 0; (b.allItems || []).forEach(function (e) {
                    if (e.a11yProxyElement) {
                        var f = e.a11yProxyElement.element, p = !1; if (b.pages && b.pages.length) { p = e.pageIx || 0; var A = e._legendItemPos ? e._legendItemPos[1] : 0; e = e.legendItem ? Math.round(e.legendItem.getBBox().height) : 0; p = A + e - b.pages[p] > c || p !== d - 1 } p ? a.styledMode ? K(f, "highcharts-a11y-invisible") : f.style.visibility =
                            "hidden" : (E(f, "highcharts-a11y-invisible"), f.style.visibility = "")
                    }
                })
            }; d.prototype.onChartRender = function () { g(this.chart) || this.removeProxies() }; d.prototype.highlightAdjacentLegendPage = function (a) { var b = this.chart, d = b.legend; a = (d.currentPage || 1) + a; var c = d.pages || []; if (0 < a && a <= c.length) { c = d.allItems.length; for (var e = 0; e < c; ++e)if (d.allItems[e].pageIx + 1 === a) { b.highlightLegendItem(e) && (this.highlightedLegendItemIx = e); break } } }; d.prototype.updateProxyPositionForItem = function (a) { a.a11yProxyElement && a.a11yProxyElement.refreshPosition() };
            d.prototype.recreateProxies = function () { this.removeProxies(); return g(this.chart) ? (this.addLegendProxyGroup(), this.proxyLegendItems(), this.updateLegendItemProxyVisibility(), this.updateLegendTitle(), !0) : !1 }; d.prototype.removeProxies = function () { this.proxyProvider.removeGroup("legend") }; d.prototype.updateLegendTitle = function () {
                var a = this.chart, b = v((a.legend && a.legend.options.title && a.legend.options.title.text || "").replace(/<br ?\/?>/g, " ")); a = a.langFormat("accessibility.legend.legendLabel" + (b ? "" : "NoTitle"),
                    { chart: a, legendTitle: b, chartTitle: u(a) }); this.proxyProvider.updateGroupAttrs("legend", { "aria-label": a })
            }; d.prototype.addLegendProxyGroup = function () { this.proxyProvider.addGroup("legend", "ul", { "aria-label": "_placeholder_", role: "all" === this.chart.options.accessibility.landmarkVerbosity ? "region" : null }) }; d.prototype.proxyLegendItems = function () { var a = this; (this.chart.legend && this.chart.legend.allItems || []).forEach(function (b) { b.legendItem && b.legendItem.element && a.proxyLegendItem(b) }) }; d.prototype.proxyLegendItem =
                function (a) { if (a.legendItem && a.legendGroup) { var b = this.chart.langFormat("accessibility.legend.legendItem", { chart: this.chart, itemName: v(a.name), item: a }); a.a11yProxyElement = this.proxyProvider.addProxyElement("legend", { click: a.legendItem, visual: (a.legendGroup.div ? a.legendItem : a.legendGroup).element }, { tabindex: -1, "aria-pressed": a.visible, "aria-label": b }) } }; d.prototype.getKeyboardNavigation = function () {
                    var a = this.keyCodes, b = this, d = this.chart; return new w(d, {
                        keyCodeMap: [[[a.left, a.right, a.up, a.down], function (a) {
                            return b.onKbdArrowKey(this,
                                a)
                        }], [[a.enter, a.space], function (d) { return h.isFirefox && d === a.space ? this.response.success : b.onKbdClick(this) }], [[a.pageDown, a.pageUp], function (d) { b.highlightAdjacentLegendPage(d === a.pageDown ? 1 : -1); return this.response.success }]], validate: function () { return b.shouldHaveLegendNavigation() }, init: function (a) { return b.onKbdNavigationInit(a) }, terminate: function () { b.highlightedLegendItemIx = -1; d.legend.allItems.forEach(function (a) { return a.setState("", !0) }) }
                    })
                }; d.prototype.onKbdArrowKey = function (a, b) {
                    var d =
                        this.keyCodes, c = a.response, e = this.chart, f = e.options.accessibility, p = e.legend.allItems.length; b = b === d.left || b === d.up ? -1 : 1; return e.highlightLegendItem(this.highlightedLegendItemIx + b) ? (this.highlightedLegendItemIx += b, c.success) : 1 < p && f.keyboardNavigation.wrapAround ? (a.init(b), c.success) : c[0 < b ? "next" : "prev"]
                }; d.prototype.onKbdClick = function (a) { var b = this.chart.legend.allItems[this.highlightedLegendItemIx]; b && b.a11yProxyElement && b.a11yProxyElement.click(); return a.response.success }; d.prototype.shouldHaveLegendNavigation =
                    function () { var a = this.chart, b = a.colorAxis && a.colorAxis.length, d = (a.options.legend || {}).accessibility || {}; return !!(a.legend && a.legend.allItems && a.legend.display && !b && d.enabled && d.keyboardNavigation && d.keyboardNavigation.enabled) }; d.prototype.onKbdNavigationInit = function (a) { var b = this.chart, d = b.legend.allItems.length - 1; a = 0 < a ? 0 : d; b.highlightLegendItem(a); this.highlightedLegendItemIx = a }; return d
        }(m); (function (a) {
            function b(a) {
                var b = this.legend.allItems, c = this.accessibility && this.accessibility.components.legend.highlightedLegendItemIx,
                f = b[a]; return f ? (d(c) && b[c] && e(b[c].legendGroup.element, "mouseout"), b = this.legend, a = b.allItems[a].pageIx, c = b.currentPage, "undefined" !== typeof a && a + 1 !== c && b.scroll(1 + a - c), a = f.legendItem, b = f.a11yProxyElement && f.a11yProxyElement.buttonElement, a && a.element && b && this.setFocusToElement(a, b), f.legendGroup && e(f.legendGroup.element, "mouseover"), !0) : !1
            } function f(a) {
                var b = a.item; this.chart.options.accessibility.enabled && b && b.a11yProxyElement && b.a11yProxyElement.buttonElement.setAttribute("aria-pressed", a.visible ?
                    "true" : "false")
            } var u = []; a.compose = function (a, d) { -1 === u.indexOf(a) && (u.push(a), a.prototype.highlightLegendItem = b); -1 === u.indexOf(d) && (u.push(d), c(d, "afterColorizeItem", f)) }
        })(a || (a = {})); return a
    }); x(a, "Accessibility/Components/SeriesComponent/SeriesDescriber.js", [a["Accessibility/Components/AnnotationsA11y.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Core/FormatUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Core/Utilities.js"]], function (a, h, t, r, m) {
        function k(a) {
            var b = a.index; return a.series &&
                a.series.data && p(b) ? x(a.series.data, function (a) { return !!(a && "undefined" !== typeof a.index && a.index > b && a.graphic && a.graphic.element) }) || null : null
        } function B(a) { var b = a.chart.options.accessibility.series.pointDescriptionEnabledThreshold; return !!(!1 !== b && a.points && a.points.length >= b) } function n(a) { var b = a.options.accessibility || {}; return !B(a) && !b.exposeAsGroupOnly } function g(a) {
            var b = a.chart.options.accessibility.keyboardNavigation.seriesNavigation; return !(!a.points || !(a.points.length < b.pointNavigationEnabledThreshold ||
                !1 === b.pointNavigationEnabledThreshold))
        } function q(a, b) { var d = a.series, c = d.chart; a = c.options.accessibility.point || {}; var e = d.options.accessibility && d.options.accessibility.point || {}; d = d.tooltipOptions || {}; c = c.options.lang; return L(b) ? D(b, e.valueDecimals || a.valueDecimals || d.valueDecimals || -1, c.decimalPoint, c.accessibility.thousandsSep || c.thousandsSep) : b } function y(a) {
            var b = (a.options.accessibility || {}).description; return b && a.chart.langFormat("accessibility.series.description", { description: b, series: a }) ||
                ""
        } function c(a, b) { return a.chart.langFormat("accessibility.series." + b + "Description", { name: v(a[b]), series: a }) } function e(a, b, d) { var c = b || "", e = d || ""; return a.series.pointArrayMap.reduce(function (b, d) { b += b.length ? ", " : ""; var p = q(a, l(a[d], a.options[d])); return b + (d + ": " + c + p + e) }, "") } function d(a) {
            var b = a.series, d = b.chart, c = a.series; var f = c.chart; var A = c.options.accessibility; A = A && A.point && A.point.valueDescriptionFormat || f.options.accessibility.point.valueDescriptionFormat; c = l(c.xAxis && c.xAxis.options.accessibility &&
                c.xAxis.options.accessibility.enabled, !f.angular); if (c) {
                    var g = a.series; var v = g.chart; var h = g.options.accessibility && g.options.accessibility.point || {}, k = v.options.accessibility.point || {}; (g = g.xAxis && g.xAxis.dateTime) ? (g = g.getXDateFormat(a.x || 0, v.options.tooltip.dateTimeLabelFormats), h = h.dateFormatter && h.dateFormatter(a) || k.dateFormatter && k.dateFormatter(a) || h.dateFormat || k.dateFormat || g, v = v.time.dateFormat(h, a.x || 0, void 0)) : v = void 0; h = (a.series.xAxis || {}).categories && p(a.category) && ("" + a.category).replace("<br/>",
                        " "); k = a.id && 0 > a.id.indexOf("highcharts-"); g = "x, " + a.x; v = a.name || v || h || (k ? a.id : g)
                } else v = ""; h = p(a.index) ? a.index + 1 : ""; k = a.series; var m = k.chart.options.accessibility.point || {}, y = k.chart.options.accessibility && k.chart.options.accessibility.point || {}, n = k.tooltipOptions || {}; g = y.valuePrefix || m.valuePrefix || n.valuePrefix || ""; m = y.valueSuffix || m.valueSuffix || n.valueSuffix || ""; y = q(a, a["undefined" !== typeof a.value ? "value" : "y"]); k = a.isNull ? k.chart.langFormat("accessibility.series.nullPointValue", { point: a }) :
                    k.pointArrayMap ? e(a, g, m) : g + y + m; f = z(A, { point: a, index: h, xDescription: v, value: k, separator: c ? ", " : "" }, f); A = (A = a.options && a.options.accessibility && a.options.accessibility.description) ? " " + A : ""; b = 1 < d.series.length && b.name ? " " + b.name + "." : ""; d = a.series.chart; c = u(a); v = { point: a, annotations: c }; d = c.length ? d.langFormat("accessibility.series.pointAnnotationsDescription", v) : ""; a.accessibility = a.accessibility || {}; a.accessibility.valueDescription = f; return f + A + b + (d ? " " + d : "")
        } function b(a) {
            var b = n(a), c = g(a); (b || c) &&
                a.points.forEach(function (c) {
                    var e; if (!(e = c.graphic && c.graphic.element) && (e = c.series && c.series.is("sunburst"), e = c.isNull && !e)) {
                        var p = c.series, f = k(c); p = (e = f && f.graphic) ? e.parentGroup : p.graph || p.group; f = f ? { x: l(c.plotX, f.plotX, 0), y: l(c.plotY, f.plotY, 0) } : { x: l(c.plotX, 0), y: l(c.plotY, 0) }; f = c.series.chart.renderer.rect(f.x, f.y, 1, 1); f.attr({ "class": "highcharts-a11y-dummy-point", fill: "none", opacity: 0, "fill-opacity": 0, "stroke-opacity": 0 }); p && p.element ? (c.graphic = f, c.hasDummyGraphic = !0, f.add(p), p.element.insertBefore(f.element,
                            e ? e.element : null), e = f.element) : e = void 0
                    } p = c.options && c.options.accessibility && !1 === c.options.accessibility.enabled; e && (e.setAttribute("tabindex", "-1"), a.chart.styledMode || (e.style.outline = "none"), b && !p ? (f = c.series, p = f.chart.options.accessibility.point || {}, f = f.options.accessibility && f.options.accessibility.point || {}, c = C(f.descriptionFormatter && f.descriptionFormatter(c) || p.descriptionFormatter && p.descriptionFormatter(c) || d(c)), e.setAttribute("role", "img"), e.setAttribute("aria-label", c)) : e.setAttribute("aria-hidden",
                        !0))
                })
        } function f(a) { var b = a.chart, d = b.types || [], e = y(a), f = function (d) { return b[d] && 1 < b[d].length && a[d] }, p = c(a, "xAxis"), A = c(a, "yAxis"), l = { name: a.name || "", ix: a.index + 1, numSeries: b.series && b.series.length, numPoints: a.points && a.points.length, series: a }; d = 1 < d.length ? "Combination" : ""; return (b.langFormat("accessibility.series.summary." + a.type + d, l) || b.langFormat("accessibility.series.summary.default" + d, l)) + (e ? " " + e : "") + (f("yAxis") ? " " + A : "") + (f("xAxis") ? " " + p : "") } var u = a.getPointAnnotationTexts, v = h.getAxisDescription,
            K = h.getSeriesFirstPointElement, E = h.getSeriesA11yElement, G = h.unhideChartElementFromAT, z = t.format, D = t.numberFormat, F = r.reverseChildNodes, C = r.stripHTMLTagsFromString, x = m.find, L = m.isNumber, l = m.pick, p = m.defined; return {
                defaultPointDescriptionFormatter: d, defaultSeriesDescriptionFormatter: f, describeSeries: function (a) {
                    var d = a.chart, c = K(a), e = E(a), p = d.is3d && d.is3d(); if (e) {
                        e.lastChild !== c || p || F(e); b(a); G(d, e); p = a.chart; d = p.options.chart; c = 1 < p.series.length; p = p.options.accessibility.series.describeSingleSeries;
                        var l = (a.options.accessibility || {}).exposeAsGroupOnly; d.options3d && d.options3d.enabled && c || !(c || p || l || B(a)) ? e.setAttribute("aria-label", "") : (d = a.chart.options.accessibility, c = d.landmarkVerbosity, (a.options.accessibility || {}).exposeAsGroupOnly ? e.setAttribute("role", "img") : "all" === c && e.setAttribute("role", "region"), e.setAttribute("tabindex", "-1"), a.chart.styledMode || (e.style.outline = "none"), e.setAttribute("aria-label", C(d.series.descriptionFormatter && d.series.descriptionFormatter(a) || f(a))))
                    }
                }
            }
    }); x(a,
        "Accessibility/Components/SeriesComponent/NewDataAnnouncer.js", [a["Core/Globals.js"], a["Core/Utilities.js"], a["Accessibility/Utils/Announcer.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/EventProvider.js"], a["Accessibility/Components/SeriesComponent/SeriesDescriber.js"]], function (a, h, t, r, m, w) {
            function k(a) { var b = a.series.data.filter(function (b) { return a.x === b.x && a.y === b.y }); return 1 === b.length ? b[0] : a } function n(a, b) {
                var d = (a || []).concat(b || []).reduce(function (a, b) {
                    a[b.name +
                    b.index] = b; return a
                }, {}); return Object.keys(d).map(function (a) { return d[a] })
            } var g = h.addEvent, q = h.defined, y = r.getChartTitle, c = w.defaultPointDescriptionFormatter, e = w.defaultSeriesDescriptionFormatter; h = function () {
                function d(a) { this.announcer = void 0; this.dirty = { allSeries: {} }; this.eventProvider = void 0; this.lastAnnouncementTime = 0; this.chart = a } d.prototype.init = function () {
                    var a = this.chart, d = a.options.accessibility.announceNewData.interruptUser ? "assertive" : "polite"; this.lastAnnouncementTime = 0; this.dirty =
                        { allSeries: {} }; this.eventProvider = new m; this.announcer = new t(a, d); this.addEventListeners()
                }; d.prototype.destroy = function () { this.eventProvider.removeAddedEvents(); this.announcer.destroy() }; d.prototype.addEventListeners = function () { var a = this, d = this.chart, c = this.eventProvider; c.addEvent(d, "afterDrilldown", function () { a.lastAnnouncementTime = 0 }); c.addEvent(d, "afterAddSeries", function (b) { a.onSeriesAdded(b.series) }); c.addEvent(d, "redraw", function () { a.announceDirtyData() }) }; d.prototype.onSeriesAdded = function (a) {
                    this.chart.options.accessibility.announceNewData.enabled &&
                    (this.dirty.hasDirty = !0, this.dirty.allSeries[a.name + a.index] = a, this.dirty.newSeries = q(this.dirty.newSeries) ? void 0 : a)
                }; d.prototype.announceDirtyData = function () { var a = this; if (this.chart.options.accessibility.announceNewData && this.dirty.hasDirty) { var d = this.dirty.newPoint; d && (d = k(d)); this.queueAnnouncement(Object.keys(this.dirty.allSeries).map(function (b) { return a.dirty.allSeries[b] }), this.dirty.newSeries, d); this.dirty = { allSeries: {} } } }; d.prototype.queueAnnouncement = function (a, d, c) {
                    var b = this, e = this.chart.options.accessibility.announceNewData;
                    if (e.enabled) {
                        var f = +new Date; e = Math.max(0, e.minAnnounceInterval - (f - this.lastAnnouncementTime)); a = n(this.queuedAnnouncement && this.queuedAnnouncement.series, a); if (d = this.buildAnnouncementMessage(a, d, c)) this.queuedAnnouncement && clearTimeout(this.queuedAnnouncementTimer), this.queuedAnnouncement = { time: f, message: d, series: a }, this.queuedAnnouncementTimer = setTimeout(function () {
                            b && b.announcer && (b.lastAnnouncementTime = +new Date, b.announcer.announce(b.queuedAnnouncement.message), delete b.queuedAnnouncement,
                                delete b.queuedAnnouncementTimer)
                        }, e)
                    }
                }; d.prototype.buildAnnouncementMessage = function (b, d, g) { var f = this.chart, u = f.options.accessibility.announceNewData; if (u.announcementFormatter && (b = u.announcementFormatter(b, d, g), !1 !== b)) return b.length ? b : null; b = a.charts && 1 < a.charts.length ? "Multiple" : "Single"; b = d ? "newSeriesAnnounce" + b : g ? "newPointAnnounce" + b : "newDataAnnounce"; u = y(f); return f.langFormat("accessibility.announceNewData." + b, { chartTitle: u, seriesDesc: d ? e(d) : null, pointDesc: g ? c(g) : null, point: g, series: d }) };
                return d
            }(); (function (a) {
                function b(a) { var b = this.chart, d = this.newDataAnnouncer; d && d.chart === b && b.options.accessibility.announceNewData.enabled && (d.dirty.newPoint = q(d.dirty.newPoint) ? void 0 : a.point) } function d() { var a = this.chart, b = this.newDataAnnouncer; b && b.chart === a && a.options.accessibility.announceNewData.enabled && (b.dirty.hasDirty = !0, b.dirty.allSeries[this.name + this.index] = this) } a.composedClasses = []; a.compose = function (c) {
                    -1 === a.composedClasses.indexOf(c) && (a.composedClasses.push(c), g(c, "addPoint",
                        b), g(c, "updatedData", d))
                }
            })(h || (h = {})); return h
        }); x(a, "Accessibility/ProxyElement.js", [a["Core/Globals.js"], a["Core/Utilities.js"], a["Accessibility/Utils/EventProvider.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"]], function (a, h, t, r, m) {
            var k = a.doc, B = h.attr, n = h.css, g = h.merge, q = r.fireEventOnWrappedOrUnwrappedElement, y = m.cloneMouseEvent, c = m.cloneTouchEvent, e = m.getFakeMouseEvent, d = m.removeElement; return function () {
                function a(a, b, d, c) {
                    this.chart = a; this.target =
                        b; this.groupType = d; d = "ul" === d; this.eventProvider = new t; var e = d ? k.createElement("li") : null, f = this.buttonElement = k.createElement("button"); a.styledMode || this.hideButtonVisually(f); e ? (d && !a.styledMode && (e.style.listStyle = "none"), e.appendChild(f), this.element = e) : this.element = f; this.updateTarget(b, c)
                } a.prototype.click = function () { var a = this.getTargetPosition(); a.x += a.width / 2; a.y += a.height / 2; a = e("click", a); q(this.target.click, a) }; a.prototype.updateTarget = function (a, b) {
                    this.target = a; this.updateCSSClassName();
                    B(this.buttonElement, g({ "aria-label": this.getTargetAttr(a.click, "aria-label") }, b)); this.eventProvider.removeAddedEvents(); this.addProxyEventsToButton(this.buttonElement, a.click); this.refreshPosition()
                }; a.prototype.refreshPosition = function () { var a = this.getTargetPosition(); n(this.buttonElement, { width: (a.width || 1) + "px", height: (a.height || 1) + "px", left: (Math.round(a.x) || 0) + "px", top: (Math.round(a.y) || 0) + "px" }) }; a.prototype.remove = function () { this.eventProvider.removeAddedEvents(); d(this.element) }; a.prototype.updateCSSClassName =
                    function () { var a = this.chart.legend; a = a.group && a.group.div; a = -1 < (a && a.className || "").indexOf("highcharts-no-tooltip"); var b = -1 < (this.getTargetAttr(this.target.click, "class") || "").indexOf("highcharts-no-tooltip"); this.buttonElement.className = a || b ? "highcharts-a11y-proxy-button highcharts-no-tooltip" : "highcharts-a11y-proxy-button" }; a.prototype.addProxyEventsToButton = function (a, b) {
                        var d = this; "click touchstart touchend touchcancel touchmove mouseover mouseenter mouseleave mouseout".split(" ").forEach(function (e) {
                            var f =
                                0 === e.indexOf("touch"); d.eventProvider.addEvent(a, e, function (a) { var d = f ? c(a) : y(a); b && q(b, d); a.stopPropagation(); f || a.preventDefault() }, { passive: !1 })
                        })
                    }; a.prototype.hideButtonVisually = function (a) { n(a, { borderWidth: 0, backgroundColor: "transparent", cursor: "pointer", outline: "none", opacity: .001, filter: "alpha(opacity=1)", zIndex: 999, overflow: "hidden", padding: 0, margin: 0, display: "block", position: "absolute", "-ms-filter": "progid:DXImageTransform.Microsoft.Alpha(Opacity=1)" }) }; a.prototype.getTargetPosition = function () {
                        var a =
                            this.target.click; a = a.element ? a.element : a; var b = this.target.visual || a; return (a = this.chart.renderTo) && b && b.getBoundingClientRect ? (b = b.getBoundingClientRect(), a = a.getBoundingClientRect(), { x: b.left - a.left, y: b.top - a.top, width: b.right - b.left, height: b.bottom - b.top }) : { x: 0, y: 0, width: 1, height: 1 }
                    }; a.prototype.getTargetAttr = function (a, b) { return a.element ? a.element.getAttribute(b) : a.getAttribute(b) }; return a
            }()
        }); x(a, "Accessibility/ProxyProvider.js", [a["Core/Globals.js"], a["Core/Utilities.js"], a["Accessibility/Utils/ChartUtilities.js"],
        a["Accessibility/Utils/DOMElementProvider.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Accessibility/ProxyElement.js"]], function (a, h, t, r, m, w) {
            var k = a.doc, n = h.attr, g = h.css, q = t.unhideChartElementFromAT, y = m.removeElement, c = m.removeChildNodes; return function () {
                function a(a) { this.chart = a; this.domElementProvider = new r; this.groups = {}; this.groupOrder = []; this.beforeChartProxyPosContainer = this.createProxyPosContainer("before"); this.afterChartProxyPosContainer = this.createProxyPosContainer("after"); this.update() }
                a.prototype.addProxyElement = function (a, b, c) { var d = this.groups[a]; if (!d) throw Error("ProxyProvider.addProxyElement: Invalid group key " + a); a = new w(this.chart, b, d.type, c); d.proxyContainerElement.appendChild(a.element); d.proxyElements.push(a); return a }; a.prototype.addGroup = function (a, b, c) {
                    if (!this.groups[a]) {
                        var d = this.domElementProvider.createElement(b); if (c && c.role && "div" !== b) { var e = this.domElementProvider.createElement("div"); e.appendChild(d) } else e = d; e.className = "highcharts-a11y-proxy-group highcharts-a11y-proxy-group-" +
                            a.replace(/\W/g, "-"); this.groups[a] = { proxyContainerElement: d, groupElement: e, type: b, proxyElements: [] }; n(e, c || {}); "ul" === b && d.setAttribute("role", "list"); this.afterChartProxyPosContainer.appendChild(e); this.updateGroupOrder(this.groupOrder)
                    }
                }; a.prototype.updateGroupAttrs = function (a, b) { var d = this.groups[a]; if (!d) throw Error("ProxyProvider.updateGroupAttrs: Invalid group key " + a); n(d.groupElement, b) }; a.prototype.updateGroupOrder = function (a) {
                    var b = this; this.groupOrder = a.slice(); if (!this.isDOMOrderGroupOrder()) {
                        var d =
                            a.indexOf("series"), e = -1 < d ? a.slice(0, d) : a, g = -1 < d ? a.slice(d + 1) : []; a = k.activeElement;["before", "after"].forEach(function (a) { var d = b["before" === a ? "beforeChartProxyPosContainer" : "afterChartProxyPosContainer"]; a = "before" === a ? e : g; c(d); a.forEach(function (a) { (a = b.groups[a]) && d.appendChild(a.groupElement) }) }); (this.beforeChartProxyPosContainer.contains(a) || this.afterChartProxyPosContainer.contains(a)) && a && a.focus && a.focus()
                    }
                }; a.prototype.clearGroup = function (a) {
                    var b = this.groups[a]; if (!b) throw Error("ProxyProvider.clearGroup: Invalid group key " +
                        a); c(b.proxyContainerElement)
                }; a.prototype.removeGroup = function (a) { var b = this.groups[a]; b && (y(b.groupElement), delete this.groups[a]) }; a.prototype.update = function () { this.updatePosContainerPositions(); this.updateGroupOrder(this.groupOrder); this.updateProxyElementPositions() }; a.prototype.updateProxyElementPositions = function () { Object.keys(this.groups).forEach(this.updateGroupProxyElementPositions.bind(this)) }; a.prototype.updateGroupProxyElementPositions = function (a) { (a = this.groups[a]) && a.proxyElements.forEach(function (a) { return a.refreshPosition() }) };
                a.prototype.destroy = function () { this.domElementProvider.destroyCreatedElements() }; a.prototype.createProxyPosContainer = function (a) { var b = this.domElementProvider.createElement("div"); b.setAttribute("aria-hidden", "false"); b.className = "highcharts-a11y-proxy-container" + (a ? "-" + a : ""); g(b, { top: "0", left: "0" }); this.chart.styledMode || (b.style.whiteSpace = "nowrap", b.style.position = "absolute"); return b }; a.prototype.getCurrentGroupOrderInDOM = function () {
                    var a = this, b = function (b) {
                        var d = []; b = b.children; for (var c = 0; c <
                            b.length; ++c) { a: { var e = b[c]; for (var f = Object.keys(a.groups), g = f.length; g--;) { var h = f[g], q = a.groups[h]; if (q && e === q.groupElement) { e = h; break a } } e = void 0 } e && d.push(e) } return d
                    }, c = b(this.beforeChartProxyPosContainer); b = b(this.afterChartProxyPosContainer); c.push("series"); return c.concat(b)
                }; a.prototype.isDOMOrderGroupOrder = function () {
                    var a = this, b = this.getCurrentGroupOrderInDOM(), c = this.groupOrder.filter(function (b) { return "series" === b || !!a.groups[b] }), e = b.length; if (e !== c.length) return !1; for (; e--;)if (b[e] !==
                        c[e]) return !1; return !0
                }; a.prototype.updatePosContainerPositions = function () { var a = this.chart, b = a.renderer.box; a.container.insertBefore(this.afterChartProxyPosContainer, b.nextSibling); a.container.insertBefore(this.beforeChartProxyPosContainer, b); q(this.chart, this.afterChartProxyPosContainer); q(this.chart, this.beforeChartProxyPosContainer) }; return a
            }()
        }); x(a, "Extensions/RangeSelector.js", [a["Core/Axis/Axis.js"], a["Core/Chart/Chart.js"], a["Core/Globals.js"], a["Core/DefaultOptions.js"], a["Core/Renderer/SVG/SVGElement.js"],
        a["Core/Utilities.js"]], function (a, h, t, r, m, w) {
            function k(a) { if (-1 !== a.indexOf("%L")) return "text"; var b = "aAdewbBmoyY".split("").some(function (b) { return -1 !== a.indexOf("%" + b) }), d = "HkIlMS".split("").some(function (b) { return -1 !== a.indexOf("%" + b) }); return b && d ? "datetime-local" : b ? "date" : d ? "time" : "text" } var n = r.defaultOptions, g = w.addEvent, q = w.createElement, y = w.css, c = w.defined, e = w.destroyObjectProperties, d = w.discardElement, b = w.extend, f = w.find, u = w.fireEvent, v = w.isNumber, x = w.merge, E = w.objectEach, G = w.pad, z = w.pick,
                D = w.pInt, F = w.splat; b(n, {
                    rangeSelector: {
                        allButtonsEnabled: !1, buttons: void 0, buttonSpacing: 5, dropdown: "responsive", enabled: void 0, verticalAlign: "top", buttonTheme: { width: 28, height: 18, padding: 2, zIndex: 7 }, floating: !1, x: 0, y: 0, height: void 0, inputBoxBorderColor: "none", inputBoxHeight: 17, inputBoxWidth: void 0, inputDateFormat: "%b %e, %Y", inputDateParser: void 0, inputEditDateFormat: "%Y-%m-%d", inputEnabled: !0, inputPosition: { align: "right", x: 0, y: 0 }, inputSpacing: 5, selected: void 0, buttonPosition: { align: "left", x: 0, y: 0 },
                        inputStyle: { color: "#335cad", cursor: "pointer" }, labelStyle: { color: "#666666" }
                    }
                }); b(n.lang, { rangeSelectorZoom: "Zoom", rangeSelectorFrom: "", rangeSelectorTo: "\u2192" }); var C = function () {
                    function f(a) { this.buttons = void 0; this.buttonOptions = f.prototype.defaultButtons; this.initialButtonGroupWidth = 0; this.options = void 0; this.chart = a; this.init(a) } f.prototype.clickButton = function (b, d) {
                        var e = this.chart, p = this.buttonOptions[b], f = e.xAxis[0], A = e.scroller && e.scroller.getUnionExtremes() || f || {}, l = A.dataMin, h = A.dataMax,
                        q = f && Math.round(Math.min(f.max, z(h, f.max))), k = p.type; A = p._range; var m, y = p.dataGrouping; if (null !== l && null !== h) {
                            e.fixedRange = A; this.setSelected(b); y && (this.forcedDataGrouping = !0, a.prototype.setDataGrouping.call(f || { chart: this.chart }, y, !1), this.frozenStates = p.preserveDataGrouping); if ("month" === k || "year" === k) if (f) { k = { range: p, max: q, chart: e, dataMin: l, dataMax: h }; var n = f.minFromRange.call(k); v(k.newMax) && (q = k.newMax) } else A = p; else if (A) n = Math.max(q - A, l), q = Math.min(n + A, h); else if ("ytd" === k) if (f) "undefined" ===
                                typeof h && (l = Number.MAX_VALUE, h = Number.MIN_VALUE, e.series.forEach(function (a) { a = a.xData; l = Math.min(a[0], l); h = Math.max(a[a.length - 1], h) }), d = !1), q = this.getYTDExtremes(h, l, e.time.useUTC), n = m = q.min, q = q.max; else { this.deferredYTDClick = b; return } else "all" === k && f && (e.navigator && e.navigator.baseSeries[0] && (e.navigator.baseSeries[0].xAxis.options.range = void 0), n = l, q = h); c(n) && (n += p._offsetMin); c(q) && (q += p._offsetMax); this.dropdown && (this.dropdown.selectedIndex = b + 1); if (f) f.setExtremes(n, q, z(d, !0), void 0, {
                                    trigger: "rangeSelectorButton",
                                    rangeSelectorButton: p
                                }); else { var t = F(e.options.xAxis)[0]; var r = t.range; t.range = A; var w = t.min; t.min = m; g(e, "load", function () { t.range = r; t.min = w }) } u(this, "afterBtnClick")
                        }
                    }; f.prototype.setSelected = function (a) { this.selected = this.options.selected = a }; f.prototype.init = function (a) {
                        var b = this, d = a.options.rangeSelector, c = d.buttons || b.defaultButtons.slice(), e = d.selected, p = function () { var a = b.minInput, d = b.maxInput; a && a.blur && u(a, "blur"); d && d.blur && u(d, "blur") }; b.chart = a; b.options = d; b.buttons = []; b.buttonOptions =
                            c; this.eventsToUnbind = []; this.eventsToUnbind.push(g(a.container, "mousedown", p)); this.eventsToUnbind.push(g(a, "resize", p)); c.forEach(b.computeButtonRange); "undefined" !== typeof e && c[e] && this.clickButton(e, !1); this.eventsToUnbind.push(g(a, "load", function () { a.xAxis && a.xAxis[0] && g(a.xAxis[0], "setExtremes", function (d) { this.max - this.min !== a.fixedRange && "rangeSelectorButton" !== d.trigger && "updatedData" !== d.trigger && b.forcedDataGrouping && !b.frozenStates && this.setDataGrouping(!1, !1) }) }))
                    }; f.prototype.updateButtonStates =
                        function () {
                            var a = this, b = this.chart, d = this.dropdown, c = b.xAxis[0], e = Math.round(c.max - c.min), f = !c.hasVisibleSeries, g = b.scroller && b.scroller.getUnionExtremes() || c, l = g.dataMin, h = g.dataMax; b = a.getYTDExtremes(h, l, b.time.useUTC); var q = b.min, k = b.max, u = a.selected, z = v(u), m = a.options.allButtonsEnabled, n = a.buttons; a.buttonOptions.forEach(function (b, p) {
                                var g = b._range, A = b.type, I = b.count || 1, M = n[p], J = 0, N = b._offsetMax - b._offsetMin; b = p === u; var v = g > h - l, y = g < c.minRange, t = !1, r = !1; g = g === e; ("month" === A || "year" === A) && e + 36E5 >=
                                    864E5 * { month: 28, year: 365 }[A] * I - N && e - 36E5 <= 864E5 * { month: 31, year: 366 }[A] * I + N ? g = !0 : "ytd" === A ? (g = k - q + N === e, t = !b) : "all" === A && (g = c.max - c.min >= h - l, r = !b && z && g); A = !m && (v || y || r || f); I = b && g || g && !z && !t || b && a.frozenStates; A ? J = 3 : I && (z = !0, J = 2); M.state !== J && (M.setState(J), d && (d.options[p + 1].disabled = A, 2 === J && (d.selectedIndex = p + 1)), 0 === J && u === p && a.setSelected())
                            })
                        }; f.prototype.computeButtonRange = function (a) {
                            var b = a.type, d = a.count || 1, c = { millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5 }; if (c[b]) a._range =
                                c[b] * d; else if ("month" === b || "year" === b) a._range = 864E5 * { month: 30, year: 365 }[b] * d; a._offsetMin = z(a.offsetMin, 0); a._offsetMax = z(a.offsetMax, 0); a._range += a._offsetMax - a._offsetMin
                        }; f.prototype.getInputValue = function (a) { a = "min" === a ? this.minInput : this.maxInput; var b = this.chart.options.rangeSelector, d = this.chart.time; return a ? ("text" === a.type && b.inputDateParser || this.defaultInputDateParser)(a.value, d.useUTC, d) : 0 }; f.prototype.setInputValue = function (a, b) {
                            var d = this.options, e = this.chart.time, p = "min" === a ? this.minInput :
                                this.maxInput; a = "min" === a ? this.minDateBox : this.maxDateBox; if (p) { var f = p.getAttribute("data-hc-time"); f = c(f) ? Number(f) : void 0; c(b) && (c(f) && p.setAttribute("data-hc-time-previous", f), p.setAttribute("data-hc-time", b), f = b); p.value = e.dateFormat(this.inputTypeFormats[p.type] || d.inputEditDateFormat, f); a && a.attr({ text: e.dateFormat(d.inputDateFormat, f) }) }
                        }; f.prototype.setInputExtremes = function (a, b, d) {
                            if (a = "min" === a ? this.minInput : this.maxInput) {
                                var c = this.inputTypeFormats[a.type], e = this.chart.time; c && (b = e.dateFormat(c,
                                    b), a.min !== b && (a.min = b), d = e.dateFormat(c, d), a.max !== d && (a.max = d))
                            }
                        }; f.prototype.showInput = function (a) {
                            var b = "min" === a ? this.minDateBox : this.maxDateBox; if ((a = "min" === a ? this.minInput : this.maxInput) && b && this.inputGroup) {
                                var d = "text" === a.type, c = this.inputGroup, e = c.translateX; c = c.translateY; var f = this.options.inputBoxWidth; y(a, { width: d ? b.width + (f ? -2 : 20) + "px" : "auto", height: d ? b.height - 2 + "px" : "auto", border: "2px solid silver" }); d && f ? y(a, { left: e + b.x + "px", top: c + "px" }) : y(a, {
                                    left: Math.min(Math.round(b.x + e - (a.offsetWidth -
                                        b.width) / 2), this.chart.chartWidth - a.offsetWidth) + "px", top: c - (a.offsetHeight - b.height) / 2 + "px"
                                })
                            }
                        }; f.prototype.hideInput = function (a) { (a = "min" === a ? this.minInput : this.maxInput) && y(a, { top: "-9999em", border: 0, width: "1px", height: "1px" }) }; f.prototype.defaultInputDateParser = function (a, b, d) {
                            var c = a.split("/").join("-").split(" ").join("T"); -1 === c.indexOf("T") && (c += "T00:00"); if (b) c += "Z"; else {
                                var e; if (e = t.isSafari) e = c, e = !(6 < e.length && (e.lastIndexOf("-") === e.length - 6 || e.lastIndexOf("+") === e.length - 6)); e && (e = (new Date(c)).getTimezoneOffset() /
                                    60, c += 0 >= e ? "+" + G(-e) + ":00" : "-" + G(e) + ":00")
                            } c = Date.parse(c); v(c) || (a = a.split("-"), c = Date.UTC(D(a[0]), D(a[1]) - 1, D(a[2]))); d && b && v(c) && (c += d.getTimezoneOffset(c)); return c
                        }; f.prototype.drawInput = function (a) {
                            function d() {
                                var b = p.getInputValue(a), d = c.xAxis[0], e = c.scroller && c.scroller.xAxis ? c.scroller.xAxis : d, f = e.dataMin; e = e.dataMax; var g = p.maxInput, l = p.minInput; b !== Number(m.getAttribute("data-hc-time-previous")) && v(b) && (m.setAttribute("data-hc-time-previous", b), u && g && v(f) ? b > Number(g.getAttribute("data-hc-time")) ?
                                    b = void 0 : b < f && (b = f) : l && v(e) && (b < Number(l.getAttribute("data-hc-time")) ? b = void 0 : b > e && (b = e)), "undefined" !== typeof b && d.setExtremes(u ? b : d.min, u ? d.max : b, void 0, void 0, { trigger: "rangeSelectorInput" }))
                            } var c = this.chart, e = this.div, f = this.inputGroup, p = this, g = c.renderer.style || {}, l = c.renderer, h = c.options.rangeSelector, u = "min" === a, z = n.lang[u ? "rangeSelectorFrom" : "rangeSelectorTo"] || ""; z = l.label(z, 0).addClass("highcharts-range-label").attr({ padding: z ? 2 : 0, height: z ? h.inputBoxHeight : 0 }).add(f); l = l.label("", 0).addClass("highcharts-range-input").attr({
                                padding: 2,
                                width: h.inputBoxWidth, height: h.inputBoxHeight, "text-align": "center"
                            }).on("click", function () { p.showInput(a); p[a + "Input"].focus() }); c.styledMode || l.attr({ stroke: h.inputBoxBorderColor, "stroke-width": 1 }); l.add(f); var m = q("input", { name: a, className: "highcharts-range-selector" }, void 0, e); m.setAttribute("type", k(h.inputDateFormat || "%b %e, %Y")); c.styledMode || (z.css(x(g, h.labelStyle)), l.css(x({ color: "#333333" }, g, h.inputStyle)), y(m, b({
                                position: "absolute", border: 0, boxShadow: "0 0 15px rgba(0,0,0,0.3)", width: "1px",
                                height: "1px", padding: 0, textAlign: "center", fontSize: g.fontSize, fontFamily: g.fontFamily, top: "-9999em"
                            }, h.inputStyle))); m.onfocus = function () { p.showInput(a) }; m.onblur = function () { m === t.doc.activeElement && d(); p.hideInput(a); p.setInputValue(a); m.blur() }; var r = !1; m.onchange = function () { r || (d(), p.hideInput(a), m.blur()) }; m.onkeypress = function (a) { 13 === a.keyCode && d() }; m.onkeydown = function (a) { r = !0; 38 !== a.keyCode && 40 !== a.keyCode || d() }; m.onkeyup = function () { r = !1 }; return { dateBox: l, input: m, label: z }
                        }; f.prototype.getPosition =
                            function () { var a = this.chart, b = a.options.rangeSelector; a = "top" === b.verticalAlign ? a.plotTop - a.axisOffset[0] : 0; return { buttonTop: a + b.buttonPosition.y, inputTop: a + b.inputPosition.y - 10 } }; f.prototype.getYTDExtremes = function (a, b, d) { var c = this.chart.time, e = new c.Date(a), f = c.get("FullYear", e); d = d ? c.Date.UTC(f, 0, 1) : +new c.Date(f, 0, 1); b = Math.max(b, d); e = e.getTime(); return { max: Math.min(a || e, e), min: b } }; f.prototype.render = function (a, b) {
                                var d = this.chart, e = d.renderer, f = d.container, p = d.options, g = p.rangeSelector, l = z(p.chart.style &&
                                    p.chart.style.zIndex, 0) + 1; p = g.inputEnabled; if (!1 !== g.enabled) {
                                        this.rendered || (this.group = e.g("range-selector-group").attr({ zIndex: 7 }).add(), this.div = q("div", void 0, { position: "relative", height: 0, zIndex: l }), this.buttonOptions.length && this.renderButtons(), f.parentNode && f.parentNode.insertBefore(this.div, f), p && (this.inputGroup = e.g("input-group").add(this.group), e = this.drawInput("min"), this.minDateBox = e.dateBox, this.minLabel = e.label, this.minInput = e.input, e = this.drawInput("max"), this.maxDateBox = e.dateBox,
                                            this.maxLabel = e.label, this.maxInput = e.input)); if (p && (this.setInputValue("min", a), this.setInputValue("max", b), a = d.scroller && d.scroller.getUnionExtremes() || d.xAxis[0] || {}, c(a.dataMin) && c(a.dataMax) && (d = d.xAxis[0].minRange || 0, this.setInputExtremes("min", a.dataMin, Math.min(a.dataMax, this.getInputValue("max")) - d), this.setInputExtremes("max", Math.max(a.dataMin, this.getInputValue("min")) + d, a.dataMax)), this.inputGroup)) {
                                                var h = 0;[this.minLabel, this.minDateBox, this.maxLabel, this.maxDateBox].forEach(function (a) {
                                                    if (a) {
                                                        var b =
                                                            a.getBBox().width; b && (a.attr({ x: h }), h += b + g.inputSpacing)
                                                    }
                                                })
                                            } this.alignElements(); this.rendered = !0
                                    }
                            }; f.prototype.renderButtons = function () {
                                var a = this, b = this.buttons, d = this.options, c = n.lang, e = this.chart.renderer, f = x(d.buttonTheme), l = f && f.states, h = f.width || 28; delete f.width; delete f.states; this.buttonGroup = e.g("range-selector-buttons").add(this.group); var k = this.dropdown = q("select", void 0, { position: "absolute", width: "1px", height: "1px", padding: 0, border: 0, top: "-9999em", cursor: "pointer", opacity: .0001 }, this.div);
                                g(k, "touchstart", function () { k.style.fontSize = "16px" });[[t.isMS ? "mouseover" : "mouseenter"], [t.isMS ? "mouseout" : "mouseleave"], ["change", "click"]].forEach(function (d) { var c = d[0], e = d[1]; g(k, c, function () { var d = b[a.currentButtonIndex()]; d && u(d.element, e || c) }) }); this.zoomText = e.label(c && c.rangeSelectorZoom || "", 0).attr({ padding: d.buttonTheme.padding, height: d.buttonTheme.height, paddingLeft: 0, paddingRight: 0 }).add(this.buttonGroup); this.chart.styledMode || (this.zoomText.css(d.labelStyle), f["stroke-width"] = z(f["stroke-width"],
                                    0)); q("option", { textContent: this.zoomText.textStr, disabled: !0 }, void 0, k); this.buttonOptions.forEach(function (d, c) { q("option", { textContent: d.title || d.text }, void 0, k); b[c] = e.button(d.text, 0, 0, function (b) { var e = d.events && d.events.click, f; e && (f = e.call(d, b)); !1 !== f && a.clickButton(c); a.isActive = !0 }, f, l && l.hover, l && l.select, l && l.disabled).attr({ "text-align": "center", width: h }).add(a.buttonGroup); d.title && b[c].attr("title", d.title) })
                            }; f.prototype.alignElements = function () {
                                var a = this, b = this.buttonGroup, d = this.buttons,
                                c = this.chart, e = this.group, f = this.inputGroup, g = this.options, l = this.zoomText, h = c.options, q = h.exporting && !1 !== h.exporting.enabled && h.navigation && h.navigation.buttonOptions; h = g.buttonPosition; var k = g.inputPosition, m = g.verticalAlign, u = function (b, d) { return q && a.titleCollision(c) && "top" === m && "right" === d.align && d.y - b.getBBox().height - 12 < (q.y || 0) + (q.height || 0) + c.spacing[0] ? -40 : 0 }, v = c.plotLeft; if (e && h && k) {
                                    var n = h.x - c.spacing[3]; if (b) {
                                        this.positionButtons(); if (!this.initialButtonGroupWidth) {
                                            var y = 0; l && (y += l.getBBox().width +
                                                5); d.forEach(function (a, b) { y += a.width; b !== d.length - 1 && (y += g.buttonSpacing) }); this.initialButtonGroupWidth = y
                                        } v -= c.spacing[3]; this.updateButtonStates(); l = u(b, h); this.alignButtonGroup(l); e.placed = b.placed = c.hasLoaded
                                    } b = 0; f && (b = u(f, k), "left" === k.align ? n = v : "right" === k.align && (n = -Math.max(c.axisOffset[1], -b)), f.align({ y: k.y, width: f.getBBox().width, align: k.align, x: k.x + n - 2 }, !0, c.spacingBox), f.placed = c.hasLoaded); this.handleCollision(b); e.align({ verticalAlign: m }, !0, c.spacingBox); f = e.alignAttr.translateY; b =
                                        e.getBBox().height + 20; u = 0; "bottom" === m && (u = (u = c.legend && c.legend.options) && "bottom" === u.verticalAlign && u.enabled && !u.floating ? c.legend.legendHeight + z(u.margin, 10) : 0, b = b + u - 20, u = f - b - (g.floating ? 0 : g.y) - (c.titleOffset ? c.titleOffset[2] : 0) - 10); if ("top" === m) g.floating && (u = 0), c.titleOffset && c.titleOffset[0] && (u = c.titleOffset[0]), u += c.margin[0] - c.spacing[0] || 0; else if ("middle" === m) if (k.y === h.y) u = f; else if (k.y || h.y) u = 0 > k.y || 0 > h.y ? u - Math.min(k.y, h.y) : f - b; e.translate(g.x, g.y + Math.floor(u)); h = this.minInput; k =
                                            this.maxInput; f = this.dropdown; g.inputEnabled && h && k && (h.style.marginTop = e.translateY + "px", k.style.marginTop = e.translateY + "px"); f && (f.style.marginTop = e.translateY + "px")
                                }
                            }; f.prototype.alignButtonGroup = function (a, b) { var d = this.chart, c = this.buttonGroup, e = this.options.buttonPosition, f = d.plotLeft - d.spacing[3], g = e.x - d.spacing[3]; "right" === e.align ? g += a - f : "center" === e.align && (g -= f / 2); c && c.align({ y: e.y, width: z(b, this.initialButtonGroupWidth), align: e.align, x: g }, !0, d.spacingBox) }; f.prototype.positionButtons = function () {
                                var a =
                                    this.buttons, b = this.chart, d = this.options, c = this.zoomText, e = b.hasLoaded ? "animate" : "attr", f = d.buttonPosition, g = b.plotLeft, l = g; c && "hidden" !== c.visibility && (c[e]({ x: z(g + f.x, g) }), l += f.x + c.getBBox().width + 5); this.buttonOptions.forEach(function (b, c) { if ("hidden" !== a[c].visibility) a[c][e]({ x: l }), l += a[c].width + d.buttonSpacing; else a[c][e]({ x: g }) })
                            }; f.prototype.handleCollision = function (a) {
                                var b = this, d = this.chart, c = this.buttonGroup, e = this.inputGroup, f = this.options, g = f.buttonPosition, p = f.dropdown, l = f.inputPosition;
                                f = function () { var a = 0; b.buttons.forEach(function (b) { b = b.getBBox(); b.width > a && (a = b.width) }); return a }; var h = function (b) { if (e && c) { var d = e.alignAttr.translateX + e.alignOptions.x - a + e.getBBox().x + 2, f = e.alignOptions.width, p = c.alignAttr.translateX + c.getBBox().x; return p + b > d && d + f > p && g.y < l.y + e.getBBox().height } return !1 }, k = function () { e && c && e.attr({ translateX: e.alignAttr.translateX + (d.axisOffset[1] >= -a ? 0 : -a), translateY: e.alignAttr.translateY + c.getBBox().height + 10 }) }; if (c) {
                                    if ("always" === p) {
                                        this.collapseButtons(a);
                                        h(f()) && k(); return
                                    } "never" === p && this.expandButtons()
                                } e && c ? l.align === g.align || h(this.initialButtonGroupWidth + 20) ? "responsive" === p ? (this.collapseButtons(a), h(f()) && k()) : k() : "responsive" === p && this.expandButtons() : c && "responsive" === p && (this.initialButtonGroupWidth > d.plotWidth ? this.collapseButtons(a) : this.expandButtons())
                            }; f.prototype.collapseButtons = function (a) {
                                var b = this.buttons, d = this.buttonOptions, c = this.chart, e = this.dropdown, f = this.options, g = this.zoomText, p = c.userOptions.rangeSelector && c.userOptions.rangeSelector.buttonTheme ||
                                    {}, l = function (a) { return { text: a ? a + " \u25be" : "\u25be", width: "auto", paddingLeft: z(f.buttonTheme.paddingLeft, p.padding, 8), paddingRight: z(f.buttonTheme.paddingRight, p.padding, 8) } }; g && g.hide(); var h = !1; d.forEach(function (a, d) { d = b[d]; 2 !== d.state ? d.hide() : (d.show(), d.attr(l(a.text)), h = !0) }); h || (e && (e.selectedIndex = 0), b[0].show(), b[0].attr(l(this.zoomText && this.zoomText.textStr))); d = f.buttonPosition.align; this.positionButtons(); "right" !== d && "center" !== d || this.alignButtonGroup(a, b[this.currentButtonIndex()].getBBox().width);
                                this.showDropdown()
                            }; f.prototype.expandButtons = function () { var a = this.buttons, b = this.buttonOptions, d = this.options, c = this.zoomText; this.hideDropdown(); c && c.show(); b.forEach(function (b, c) { c = a[c]; c.show(); c.attr({ text: b.text, width: d.buttonTheme.width || 28, paddingLeft: z(d.buttonTheme.paddingLeft, "unset"), paddingRight: z(d.buttonTheme.paddingRight, "unset") }); 2 > c.state && c.setState(0) }); this.positionButtons() }; f.prototype.currentButtonIndex = function () {
                                var a = this.dropdown; return a && 0 < a.selectedIndex ? a.selectedIndex -
                                    1 : 0
                            }; f.prototype.showDropdown = function () { var a = this.buttonGroup, b = this.buttons, d = this.chart, c = this.dropdown; if (a && c) { var e = a.translateX; a = a.translateY; b = b[this.currentButtonIndex()].getBBox(); y(c, { left: d.plotLeft + e + "px", top: a + .5 + "px", width: b.width + "px", height: b.height + "px" }); this.hasVisibleDropdown = !0 } }; f.prototype.hideDropdown = function () { var a = this.dropdown; a && (y(a, { top: "-9999em", width: "1px", height: "1px" }), this.hasVisibleDropdown = !1) }; f.prototype.getHeight = function () {
                                var a = this.options, b = this.group,
                                d = a.y, c = a.buttonPosition.y, e = a.inputPosition.y; if (a.height) return a.height; this.alignElements(); a = b ? b.getBBox(!0).height + 13 + d : 0; b = Math.min(e, c); if (0 > e && 0 > c || 0 < e && 0 < c) a += Math.abs(b); return a
                            }; f.prototype.titleCollision = function (a) { return !(a.options.title.text || a.options.subtitle.text) }; f.prototype.update = function (a) { var b = this.chart; x(!0, b.options.rangeSelector, a); this.destroy(); this.init(b); this.render() }; f.prototype.destroy = function () {
                                var a = this, b = a.minInput, c = a.maxInput; a.eventsToUnbind && (a.eventsToUnbind.forEach(function (a) { return a() }),
                                    a.eventsToUnbind = void 0); e(a.buttons); b && (b.onfocus = b.onblur = b.onchange = null); c && (c.onfocus = c.onblur = c.onchange = null); E(a, function (b, c) { b && "chart" !== c && (b instanceof m ? b.destroy() : b instanceof window.HTMLElement && d(b)); b !== f.prototype[c] && (a[c] = null) }, this)
                            }; return f
                }(); C.prototype.defaultButtons = [{ type: "month", count: 1, text: "1m", title: "View 1 month" }, { type: "month", count: 3, text: "3m", title: "View 3 months" }, { type: "month", count: 6, text: "6m", title: "View 6 months" }, { type: "ytd", text: "YTD", title: "View year to date" },
                { type: "year", count: 1, text: "1y", title: "View 1 year" }, { type: "all", text: "All", title: "View all" }]; C.prototype.inputTypeFormats = { "datetime-local": "%Y-%m-%dT%H:%M:%S", date: "%Y-%m-%d", time: "%H:%M:%S" }; a.prototype.minFromRange = function () {
                    var a = this.range, b = a.type, d = this.max, c = this.chart.time, e = function (a, d) { var e = "year" === b ? "FullYear" : "Month", f = new c.Date(a), g = c.get(e, f); c.set(e, f, g + d); g === c.get(e, f) && c.set("Date", f, 0); return f.getTime() - a }; if (v(a)) { var f = d - a; var g = a } else f = d + e(d, -a.count), this.chart && (this.chart.fixedRange =
                        d - f); var h = z(this.dataMin, Number.MIN_VALUE); v(f) || (f = h); f <= h && (f = h, "undefined" === typeof g && (g = e(f, a.count)), this.newMax = Math.min(f + g, this.dataMax)); v(d) || (f = void 0); return f
                }; if (!t.RangeSelector) {
                    var H = [], L = function (a) {
                        function b() { c && (d = a.xAxis[0].getExtremes(), e = a.legend, h = c && c.options.verticalAlign, v(d.min) && c.render(d.min, d.max), e.display && "top" === h && h === e.options.verticalAlign && (l = x(a.spacingBox), l.y = "vertical" === e.options.layout ? a.plotTop : l.y + c.getHeight(), e.group.placed = !1, e.align(l))) } var d,
                            c = a.rangeSelector, e, l, h; c && (f(H, function (b) { return b[0] === a }) || H.push([a, [g(a.xAxis[0], "afterSetExtremes", function (a) { c && c.render(a.min, a.max) }), g(a, "redraw", b)]]), b())
                    }; g(h, "afterGetContainer", function () { this.options.rangeSelector && this.options.rangeSelector.enabled && (this.rangeSelector = new C(this)) }); g(h, "beforeRender", function () {
                        var a = this.axes, b = this.rangeSelector; b && (v(b.deferredYTDClick) && (b.clickButton(b.deferredYTDClick), delete b.deferredYTDClick), a.forEach(function (a) { a.updateNames(); a.setScale() }),
                            this.getAxisMargins(), b.render(), a = b.options.verticalAlign, b.options.floating || ("bottom" === a ? this.extraBottomMargin = !0 : "middle" !== a && (this.extraTopMargin = !0)))
                    }); g(h, "update", function (a) {
                        var b = a.options.rangeSelector; a = this.rangeSelector; var d = this.extraBottomMargin, e = this.extraTopMargin; b && b.enabled && !c(a) && this.options.rangeSelector && (this.options.rangeSelector.enabled = !0, this.rangeSelector = a = new C(this)); this.extraTopMargin = this.extraBottomMargin = !1; a && (L(this), b = b && b.verticalAlign || a.options &&
                            a.options.verticalAlign, a.options.floating || ("bottom" === b ? this.extraBottomMargin = !0 : "middle" !== b && (this.extraTopMargin = !0)), this.extraBottomMargin !== d || this.extraTopMargin !== e) && (this.isDirtyBox = !0)
                    }); g(h, "render", function () { var a = this.rangeSelector; a && !a.options.floating && (a.render(), a = a.options.verticalAlign, "bottom" === a ? this.extraBottomMargin = !0 : "middle" !== a && (this.extraTopMargin = !0)) }); g(h, "getMargins", function () {
                        var a = this.rangeSelector; a && (a = a.getHeight(), this.extraTopMargin && (this.plotTop +=
                            a), this.extraBottomMargin && (this.marginBottom += a))
                    }); h.prototype.callbacks.push(L); g(h, "destroy", function () { for (var a = 0; a < H.length; a++) { var b = H[a]; if (b[0] === this) { b[1].forEach(function (a) { return a() }); H.splice(a, 1); break } } }); t.RangeSelector = C
                } return C
        }); x(a, "Accessibility/Components/RangeSelectorComponent.js", [a["Extensions/RangeSelector.js"], a["Accessibility/AccessibilityComponent.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/Announcer.js"], a["Accessibility/KeyboardNavigationHandler.js"],
        a["Core/Utilities.js"]], function (a, h, t, r, m, w) {
            var k = this && this.__extends || function () { var a = function (c, d) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, d) { a.__proto__ = d } || function (a, d) { for (var b in d) d.hasOwnProperty(b) && (a[b] = d[b]) }; return a(c, d) }; return function (c, d) { function b() { this.constructor = c } a(c, d); c.prototype = null === d ? Object.create(d) : (b.prototype = d.prototype, new b) } }(), n = t.unhideChartElementFromAT, g = t.getAxisRangeDescription, q = w.addEvent, y = w.attr; h = function (a) {
                function c() {
                    var d =
                        null !== a && a.apply(this, arguments) || this; d.announcer = void 0; return d
                } k(c, a); c.prototype.init = function () { this.announcer = new r(this.chart, "polite") }; c.prototype.onChartUpdate = function () {
                    var a = this.chart, b = this, c = a.rangeSelector; c && (this.updateSelectorVisibility(), this.setDropdownAttrs(), c.buttons && c.buttons.length && c.buttons.forEach(function (a) { b.setRangeButtonAttrs(a) }), c.maxInput && c.minInput && ["minInput", "maxInput"].forEach(function (d, e) {
                        if (d = c[d]) n(a, d), b.setRangeInputAttrs(d, "accessibility.rangeSelector." +
                            (e ? "max" : "min") + "InputLabel")
                    }))
                }; c.prototype.updateSelectorVisibility = function () { var a = this.chart, b = a.rangeSelector, c = b && b.dropdown, e = b && b.buttons || []; b && b.hasVisibleDropdown && c ? (n(a, c), e.forEach(function (a) { return a.element.setAttribute("aria-hidden", !0) })) : (c && c.setAttribute("aria-hidden", !0), e.forEach(function (b) { return n(a, b.element) })) }; c.prototype.setDropdownAttrs = function () {
                    var a = this.chart, b = a.rangeSelector && a.rangeSelector.dropdown; b && (a = a.langFormat("accessibility.rangeSelector.dropdownLabel",
                        { rangeTitle: a.options.lang.rangeSelectorZoom }), b.setAttribute("aria-label", a), b.setAttribute("tabindex", -1))
                }; c.prototype.setRangeButtonAttrs = function (a) { y(a.element, { tabindex: -1, role: "button" }) }; c.prototype.setRangeInputAttrs = function (a, b) { var c = this.chart; y(a, { tabindex: -1, "aria-label": c.langFormat(b, { chart: c }) }) }; c.prototype.onButtonNavKbdArrowKey = function (a, b) {
                    var c = a.response, d = this.keyCodes, e = this.chart, g = e.options.accessibility.keyboardNavigation.wrapAround; b = b === d.left || b === d.up ? -1 : 1; return e.highlightRangeSelectorButton(e.highlightedRangeSelectorItemIx +
                        b) ? c.success : g ? (a.init(b), c.success) : c[0 < b ? "next" : "prev"]
                }; c.prototype.onButtonNavKbdClick = function (a) { a = a.response; var b = this.chart; 3 !== b.oldRangeSelectorItemState && this.fakeClickEvent(b.rangeSelector.buttons[b.highlightedRangeSelectorItemIx].element); return a.success }; c.prototype.onAfterBtnClick = function () { var a = this.chart, b = g(a.xAxis[0]); (a = a.langFormat("accessibility.rangeSelector.clickButtonAnnouncement", { chart: a, axisRangeDescription: b })) && this.announcer.announce(a) }; c.prototype.onInputKbdMove =
                    function (a) { var b = this.chart, c = b.rangeSelector, d = b.highlightedInputRangeIx = (b.highlightedInputRangeIx || 0) + a; 1 < d || 0 > d ? b.accessibility && (b.accessibility.keyboardNavigation.tabindexContainer.focus(), b.accessibility.keyboardNavigation[0 > a ? "prev" : "next"]()) : c && (a = c[d ? "maxDateBox" : "minDateBox"], c = c[d ? "maxInput" : "minInput"], a && c && b.setFocusToElement(a, c)) }; c.prototype.onInputNavInit = function (a) {
                        var b = this, c = this, d = this.chart, e = 0 < a ? 0 : 1, g = d.rangeSelector, h = g && g[e ? "maxDateBox" : "minDateBox"]; a = g && g.minInput;
                        g = g && g.maxInput; d.highlightedInputRangeIx = e; if (h && a && g) { d.setFocusToElement(h, e ? g : a); this.removeInputKeydownHandler && this.removeInputKeydownHandler(); d = function (a) { (a.which || a.keyCode) === b.keyCodes.tab && (a.preventDefault(), a.stopPropagation(), c.onInputKbdMove(a.shiftKey ? -1 : 1)) }; var k = q(a, "keydown", d), m = q(g, "keydown", d); this.removeInputKeydownHandler = function () { k(); m() } }
                    }; c.prototype.onInputNavTerminate = function () {
                        var a = this.chart.rangeSelector || {}; a.maxInput && a.hideInput("max"); a.minInput && a.hideInput("min");
                        this.removeInputKeydownHandler && (this.removeInputKeydownHandler(), delete this.removeInputKeydownHandler)
                    }; c.prototype.initDropdownNav = function () {
                        var a = this, b = this.chart, c = b.rangeSelector, e = c && c.dropdown; c && e && (b.setFocusToElement(c.buttonGroup, e), this.removeDropdownKeydownHandler && this.removeDropdownKeydownHandler(), this.removeDropdownKeydownHandler = q(e, "keydown", function (c) {
                            var d = b.accessibility; (c.which || c.keyCode) === a.keyCodes.tab && (c.preventDefault(), c.stopPropagation(), d && (d.keyboardNavigation.tabindexContainer.focus(),
                                d.keyboardNavigation[c.shiftKey ? "prev" : "next"]()))
                        }))
                    }; c.prototype.getRangeSelectorButtonNavigation = function () {
                        var a = this.chart, b = this.keyCodes, c = this; return new m(a, {
                            keyCodeMap: [[[b.left, b.right, b.up, b.down], function (a) { return c.onButtonNavKbdArrowKey(this, a) }], [[b.enter, b.space], function () { return c.onButtonNavKbdClick(this) }]], validate: function () { return !!(a.rangeSelector && a.rangeSelector.buttons && a.rangeSelector.buttons.length) }, init: function (b) {
                                var d = a.rangeSelector; d && d.hasVisibleDropdown ? c.initDropdownNav() :
                                    d && (d = d.buttons.length - 1, a.highlightRangeSelectorButton(0 < b ? 0 : d))
                            }, terminate: function () { c.removeDropdownKeydownHandler && (c.removeDropdownKeydownHandler(), delete c.removeDropdownKeydownHandler) }
                        })
                    }; c.prototype.getRangeSelectorInputNavigation = function () {
                        var a = this.chart, b = this; return new m(a, {
                            keyCodeMap: [], validate: function () {
                                return !!(a.rangeSelector && a.rangeSelector.inputGroup && "hidden" !== a.rangeSelector.inputGroup.element.style.visibility && !1 !== a.options.rangeSelector.inputEnabled && a.rangeSelector.minInput &&
                                    a.rangeSelector.maxInput)
                            }, init: function (a) { b.onInputNavInit(a) }, terminate: function () { b.onInputNavTerminate() }
                        })
                    }; c.prototype.getKeyboardNavigation = function () { return [this.getRangeSelectorButtonNavigation(), this.getRangeSelectorInputNavigation()] }; c.prototype.destroy = function () { this.removeDropdownKeydownHandler && this.removeDropdownKeydownHandler(); this.removeInputKeydownHandler && this.removeInputKeydownHandler(); this.announcer && this.announcer.destroy() }; return c
            }(h); (function (c) {
                function e(a) {
                    var b =
                        this.rangeSelector && this.rangeSelector.buttons || [], c = this.highlightedRangeSelectorItemIx, d = this.rangeSelector && this.rangeSelector.selected; "undefined" !== typeof c && b[c] && c !== d && b[c].setState(this.oldRangeSelectorItemState || 0); this.highlightedRangeSelectorItemIx = a; return b[a] ? (this.setFocusToElement(b[a].box, b[a].element), a !== d && (this.oldRangeSelectorItemState = b[a].state, b[a].setState(1)), !0) : !1
                } function d() { var a = this.chart.accessibility; if (a && a.components.rangeSelector) return a.components.rangeSelector.onAfterBtnClick() }
                var b = []; c.compose = function (c, g) { -1 === b.indexOf(c) && (b.push(c), c.prototype.highlightRangeSelectorButton = e); -1 === b.indexOf(g) && (b.push(g), q(a, "afterBtnClick", d)) }
            })(h || (h = {})); return h
        }); x(a, "Accessibility/Components/SeriesComponent/ForcedMarkers.js", [a["Core/Utilities.js"]], function (a) {
            var h = a.addEvent, k = a.merge, r; (function (a) {
                function m(a) { k(!0, a, { marker: { enabled: !0, states: { normal: { opacity: 0 } } } }) } function r(a) { return a.marker.states && a.marker.states.normal && a.marker.states.normal.opacity } function n() {
                    if (this.chart.styledMode) {
                        if (this.markerGroup) this.markerGroup[this.a11yMarkersForced ?
                            "addClass" : "removeClass"]("highcharts-a11y-markers-hidden"); this._hasPointMarkers && this.points && this.points.length && this.points.forEach(function (a) { a.graphic && (a.graphic[a.hasForcedA11yMarker ? "addClass" : "removeClass"]("highcharts-a11y-marker-hidden"), a.graphic[!1 === a.hasForcedA11yMarker ? "addClass" : "removeClass"]("highcharts-a11y-marker-visible")) })
                    }
                } function g(a) { this.resetA11yMarkerOptions = k(a.options.marker || {}, this.userOptions.marker || {}) } function q() {
                    var a = this.options, e = !1 !== (this.options.accessibility &&
                        this.options.accessibility.enabled); if (e = this.chart.options.accessibility.enabled && e) e = this.chart.options.accessibility, e = this.points.length < e.series.pointDescriptionEnabledThreshold || !1 === e.series.pointDescriptionEnabledThreshold; if (e) {
                            if (a.marker && !1 === a.marker.enabled && (this.a11yMarkersForced = !0, m(this.options)), this._hasPointMarkers && this.points && this.points.length) for (a = this.points.length; a--;) {
                                e = this.points[a]; var d = e.options, b = e.hasForcedA11yMarker; delete e.hasForcedA11yMarker; d.marker && (b =
                                    b && 0 === r(d), d.marker.enabled && !b ? (k(!0, d.marker, { states: { normal: { opacity: r(d) || 1 } } }), e.hasForcedA11yMarker = !1) : !1 === d.marker.enabled && (m(d), e.hasForcedA11yMarker = !0))
                            }
                        } else this.a11yMarkersForced && (delete this.a11yMarkersForced, (a = this.resetA11yMarkerOptions) && k(!0, this.options, { marker: { enabled: a.enabled, states: { normal: { opacity: a.states && a.states.normal && a.states.normal.opacity } } } }))
                } var y = []; a.compose = function (a) {
                    -1 === y.indexOf(a) && (y.push(a), h(a, "afterSetOptions", g), h(a, "render", q), h(a, "afterRender",
                        n))
                }
            })(r || (r = {})); return r
        }); x(a, "Accessibility/Components/SeriesComponent/SeriesKeyboardNavigation.js", [a["Core/Series/Point.js"], a["Core/Series/Series.js"], a["Core/Series/SeriesRegistry.js"], a["Core/Globals.js"], a["Core/Utilities.js"], a["Accessibility/KeyboardNavigationHandler.js"], a["Accessibility/Utils/EventProvider.js"], a["Accessibility/Utils/ChartUtilities.js"]], function (a, h, t, r, m, w, x, n) {
            function g(a) { var b = a.index, c = a.series.points, d = c.length; if (c[b] !== a) for (; d--;) { if (c[d] === a) return d } else return b }
            function k(a) { var b = a.chart.options.accessibility.keyboardNavigation.seriesNavigation, c = a.options.accessibility || {}, d = c.keyboardNavigation; return d && !1 === d.enabled || !1 === c.enabled || !1 === a.options.enableMouseTracking || !a.visible || b.pointNavigationEnabledThreshold && b.pointNavigationEnabledThreshold <= a.points.length } function y(a) {
                var b = a.series.chart.options.accessibility, c = a.options.accessibility && !1 === a.options.accessibility.enabled; return a.isNull && b.keyboardNavigation.seriesNavigation.skipNullPoints ||
                    !1 === a.visible || !1 === a.isInside || c || k(a.series)
            } function c(a) { var b = !1; delete a.highlightedPoint; return b = a.series.reduce(function (a, b) { return a || b.highlightFirstValidPoint() }, !1) } var e = t.seriesTypes, d = r.doc, b = m.defined, f = m.fireEvent, u = n.getPointFromXY, v = n.getSeriesFromName, B = n.scrollToPoint; t = function () {
                function e(a, b) { this.keyCodes = b; this.chart = a } e.prototype.init = function () {
                    var b = this, e = this.chart, f = this.eventProvider = new x; f.addEvent(h, "destroy", function () { return b.onSeriesDestroy(this) }); f.addEvent(e,
                        "afterDrilldown", function () { c(this); this.focusElement && this.focusElement.removeFocusBorder() }); f.addEvent(e, "drilldown", function (a) { a = a.point; var c = a.series; b.lastDrilledDownPoint = { x: a.x, y: a.y, seriesName: c ? c.name : "" } }); f.addEvent(e, "drillupall", function () { setTimeout(function () { b.onDrillupAll() }, 10) }); f.addEvent(a, "afterSetState", function () {
                            var a = this.graphic && this.graphic.element, b = d.activeElement, c = b && b.getAttribute("class"); c = c && -1 < c.indexOf("highcharts-a11y-proxy-button"); e.highlightedPoint ===
                                this && b !== a && !c && a && a.focus && a.focus()
                        })
                }; e.prototype.onDrillupAll = function () { var a = this.lastDrilledDownPoint, c = this.chart, d = a && v(c, a.seriesName), e; a && d && b(a.x) && b(a.y) && (e = u(d, a.x, a.y)); c.container && c.container.focus(); e && e.highlight && e.highlight(); c.focusElement && c.focusElement.removeFocusBorder() }; e.prototype.getKeyboardNavigationHandler = function () {
                    var a = this, b = this.keyCodes, d = this.chart, e = d.inverted; return new w(d, {
                        keyCodeMap: [[e ? [b.up, b.down] : [b.left, b.right], function (b) {
                            return a.onKbdSideways(this,
                                b)
                        }], [e ? [b.left, b.right] : [b.up, b.down], function (b) { return a.onKbdVertical(this, b) }], [[b.enter, b.space], function (a, b) { if (a = d.highlightedPoint) b.point = a, f(a.series, "click", b), a.firePointEvent("click"); return this.response.success }], [[b.home], function () { c(d); return this.response.success }], [[b.end], function () { for (var a = d.series.length, b; a-- && !(d.highlightedPoint = d.series[a].points[d.series[a].points.length - 1], b = d.series[a].highlightFirstValidPoint());); return this.response.success }], [[b.pageDown, b.pageUp],
                        function (a) { d.highlightAdjacentSeries(a === b.pageDown); return this.response.success }]], init: function () { c(d); return this.response.success }, terminate: function () { return a.onHandlerTerminate() }
                    })
                }; e.prototype.onKbdSideways = function (a, b) { var c = this.keyCodes; return this.attemptHighlightAdjacentPoint(a, b === c.right || b === c.down) }; e.prototype.onKbdVertical = function (a, b) {
                    var c = this.chart, d = this.keyCodes; b = b === d.down || b === d.right; d = c.options.accessibility.keyboardNavigation.seriesNavigation; if (d.mode && "serialize" ===
                        d.mode) return this.attemptHighlightAdjacentPoint(a, b); c[c.highlightedPoint && c.highlightedPoint.series.keyboardMoveVertical ? "highlightAdjacentPointVertical" : "highlightAdjacentSeries"](b); return a.response.success
                }; e.prototype.onHandlerTerminate = function () { var a = this.chart; a.tooltip && a.tooltip.hide(0); var b = a.highlightedPoint && a.highlightedPoint.series; if (b && b.onMouseOut) b.onMouseOut(); if (a.highlightedPoint && a.highlightedPoint.onMouseOut) a.highlightedPoint.onMouseOut(); delete a.highlightedPoint }; e.prototype.attemptHighlightAdjacentPoint =
                    function (a, b) { var c = this.chart, d = c.options.accessibility.keyboardNavigation.wrapAround; return c.highlightAdjacentPoint(b) ? a.response.success : d ? a.init(b ? 1 : -1) : a.response[b ? "next" : "prev"] }; e.prototype.onSeriesDestroy = function (a) { var b = this.chart; b.highlightedPoint && b.highlightedPoint.series === a && (delete b.highlightedPoint, b.focusElement && b.focusElement.removeFocusBorder()) }; e.prototype.destroy = function () { this.eventProvider.removeAddedEvents() }; return e
            }(); (function (a) {
                function c(a) {
                    var b = this.series,
                    c = this.highlightedPoint, d = c && g(c) || 0, e = c && c.series.points || [], f = this.series && this.series[this.series.length - 1]; f = f && f.points && f.points[f.points.length - 1]; if (!b[0] || !b[0].points) return !1; if (c) { if (b = b[c.series.index + (a ? 1 : -1)], d = e[d + (a ? 1 : -1)], !d && b && (d = b.points[a ? 0 : b.points.length - 1]), !d) return !1 } else d = a ? b[0].points[0] : f; return y(d) ? (b = d.series, k(b) ? this.highlightedPoint = a ? b.points[b.points.length - 1] : b.points[0] : this.highlightedPoint = d, this.highlightAdjacentPoint(a)) : d.highlight()
                } function d(a) {
                    var c =
                        this.highlightedPoint, d = Infinity, e; if (!b(c.plotX) || !b(c.plotY)) return !1; this.series.forEach(function (f) { k(f) || f.points.forEach(function (g) { if (b(g.plotY) && b(g.plotX) && g !== c) { var h = g.plotY - c.plotY, k = Math.abs(g.plotX - c.plotX); k = Math.abs(h) * Math.abs(h) + k * k * 4; f.yAxis && f.yAxis.reversed && (h *= -1); !(0 >= h && a || 0 <= h && !a || 5 > k || y(g)) && k < d && (d = k, e = g) } }) }); return e ? e.highlight() : !1
                } function f(a) {
                    var b = this.highlightedPoint, c = this.series && this.series[this.series.length - 1], d = c && c.points && c.points[c.points.length - 1];
                    if (!this.highlightedPoint) return c = a ? this.series && this.series[0] : c, (d = a ? c && c.points && c.points[0] : d) ? d.highlight() : !1; c = this.series[b.series.index + (a ? -1 : 1)]; if (!c) return !1; d = h(b, c, 4); if (!d) return !1; if (k(c)) return d.highlight(), a = this.highlightAdjacentSeries(a), a ? a : (b.highlight(), !1); d.highlight(); return d.series.highlightFirstValidPoint()
                } function h(a, c, d, e) {
                    var f = Infinity, g = c.points.length, h = function (a) { return !(b(a.plotX) && b(a.plotY)) }; if (!h(a)) {
                        for (; g--;) {
                            var k = c.points[g]; if (!h(k) && (k = (a.plotX -
                                k.plotX) * (a.plotX - k.plotX) * (d || 1) + (a.plotY - k.plotY) * (a.plotY - k.plotY) * (e || 1), k < f)) { f = k; var l = g }
                        } return b(l) ? c.points[l] : void 0
                    }
                } function q() { var a = this.series.chart; if (this.isNull) a.tooltip && a.tooltip.hide(0); else this.onMouseOver(); B(this); this.graphic && a.setFocusToElement(this.graphic); a.highlightedPoint = this; return this } function m() {
                    var a = this.chart.highlightedPoint, b = (a && a.series) === this ? g(a) : 0; a = this.points; var c = a.length; if (a && c) {
                        for (var d = b; d < c; ++d)if (!y(a[d])) return a[d].highlight(); for (; 0 <=
                            b; --b)if (!y(a[b])) return a[b].highlight()
                    } return !1
                } var n = []; a.compose = function (a, b, g) { -1 === n.indexOf(a) && (n.push(a), a = a.prototype, a.highlightAdjacentPoint = c, a.highlightAdjacentPointVertical = d, a.highlightAdjacentSeries = f); -1 === n.indexOf(b) && (n.push(b), b.prototype.highlight = q); -1 === n.indexOf(g) && (n.push(g), b = g.prototype, b.keyboardMoveVertical = !0, ["column", "gantt", "pie"].forEach(function (a) { e[a] && (e[a].prototype.keyboardMoveVertical = !1) }), b.highlightFirstValidPoint = m) }
            })(t || (t = {})); return t
        }); x(a, "Accessibility/Components/SeriesComponent/SeriesComponent.js",
            [a["Accessibility/AccessibilityComponent.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Components/SeriesComponent/ForcedMarkers.js"], a["Accessibility/Components/SeriesComponent/NewDataAnnouncer.js"], a["Accessibility/Components/SeriesComponent/SeriesDescriber.js"], a["Accessibility/Components/SeriesComponent/SeriesKeyboardNavigation.js"], a["Core/Tooltip.js"]], function (a, h, t, r, m, w, x) {
                var k = this && this.__extends || function () {
                    var a = function (c, e) {
                        a = Object.setPrototypeOf || { __proto__: [] } instanceof
                        Array && function (a, b) { a.__proto__ = b } || function (a, b) { for (var c in b) b.hasOwnProperty(c) && (a[c] = b[c]) }; return a(c, e)
                    }; return function (c, e) { function d() { this.constructor = c } a(c, e); c.prototype = null === e ? Object.create(e) : (d.prototype = e.prototype, new d) }
                }(), g = h.hideSeriesFromAT, q = m.describeSeries; return function (a) {
                    function c() { return null !== a && a.apply(this, arguments) || this } k(c, a); c.compose = function (a, c, b) { r.compose(b); t.compose(b); w.compose(a, c, b) }; c.prototype.init = function () {
                        this.newDataAnnouncer = new r(this.chart);
                        this.newDataAnnouncer.init(); this.keyboardNavigation = new w(this.chart, this.keyCodes); this.keyboardNavigation.init(); this.hideTooltipFromATWhenShown(); this.hideSeriesLabelsFromATWhenShown()
                    }; c.prototype.hideTooltipFromATWhenShown = function () { var a = this; this.addEvent(x, "refresh", function () { this.chart === a.chart && this.label && this.label.element && this.label.element.setAttribute("aria-hidden", !0) }) }; c.prototype.hideSeriesLabelsFromATWhenShown = function () {
                        this.addEvent(this.chart, "afterDrawSeriesLabels", function () {
                            this.series.forEach(function (a) {
                                a.labelBySeries &&
                                a.labelBySeries.attr("aria-hidden", !0)
                            })
                        })
                    }; c.prototype.onChartRender = function () { this.chart.series.forEach(function (a) { !1 !== (a.options.accessibility && a.options.accessibility.enabled) && a.visible ? q(a) : g(a) }) }; c.prototype.getKeyboardNavigation = function () { return this.keyboardNavigation.getKeyboardNavigationHandler() }; c.prototype.destroy = function () { this.newDataAnnouncer.destroy(); this.keyboardNavigation.destroy() }; return c
                }(a)
            }); x(a, "Accessibility/Components/ZoomComponent.js", [a["Accessibility/AccessibilityComponent.js"],
            a["Accessibility/Utils/ChartUtilities.js"], a["Core/Globals.js"], a["Accessibility/KeyboardNavigationHandler.js"], a["Core/Utilities.js"]], function (a, h, t, r, m) {
                var k = this && this.__extends || function () {
                    var a = function (g, c) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, c) { a.__proto__ = c } || function (a, c) { for (var b in c) c.hasOwnProperty(b) && (a[b] = c[b]) }; return a(g, c) }; return function (g, c) {
                        function e() { this.constructor = g } a(g, c); g.prototype = null === c ? Object.create(c) : (e.prototype = c.prototype,
                            new e)
                    }
                }(), x = h.unhideChartElementFromAT, n = m.attr, g = m.pick; a = function (a) {
                    function h() { var c = null !== a && a.apply(this, arguments) || this; c.focusedMapNavButtonIx = -1; return c } k(h, a); h.prototype.init = function () { var a = this, e = this.chart; this.proxyProvider.addGroup("zoom", "div");["afterShowResetZoom", "afterDrilldown", "drillupall"].forEach(function (c) { a.addEvent(e, c, function () { a.updateProxyOverlays() }) }) }; h.prototype.onChartUpdate = function () {
                        var a = this.chart, e = this; a.mapNavButtons && a.mapNavButtons.forEach(function (c,
                            b) { x(a, c.element); e.setMapNavButtonAttrs(c.element, "accessibility.zoom.mapZoom" + (b ? "Out" : "In")) })
                    }; h.prototype.setMapNavButtonAttrs = function (a, e) { var c = this.chart; e = c.langFormat(e, { chart: c }); n(a, { tabindex: -1, role: "button", "aria-label": e }) }; h.prototype.onChartRender = function () { this.updateProxyOverlays() }; h.prototype.updateProxyOverlays = function () {
                        var a = this.chart; this.proxyProvider.clearGroup("zoom"); a.resetZoomButton && this.createZoomProxyButton(a.resetZoomButton, "resetZoomProxyButton", a.langFormat("accessibility.zoom.resetZoomButton",
                            { chart: a })); a.drillUpButton && this.createZoomProxyButton(a.drillUpButton, "drillUpProxyButton", a.langFormat("accessibility.drillUpButton", { chart: a, buttonText: a.getDrilldownBackText() }))
                    }; h.prototype.createZoomProxyButton = function (a, e, d) { this[e] = this.proxyProvider.addProxyElement("zoom", { click: a }, { "aria-label": d, tabindex: -1 }) }; h.prototype.getMapZoomNavigation = function () {
                        var a = this.keyCodes, e = this.chart, d = this; return new r(e, {
                            keyCodeMap: [[[a.up, a.down, a.left, a.right], function (a) {
                                return d.onMapKbdArrow(this,
                                    a)
                            }], [[a.tab], function (a, c) { return d.onMapKbdTab(this, c) }], [[a.space, a.enter], function () { return d.onMapKbdClick(this) }]], validate: function () { return !!(e.mapZoom && e.mapNavButtons && e.mapNavButtons.length) }, init: function (a) { return d.onMapNavInit(a) }
                        })
                    }; h.prototype.onMapKbdArrow = function (a, e) { var c = this.keyCodes; this.chart[e === c.up || e === c.down ? "yAxis" : "xAxis"][0].panStep(e === c.left || e === c.up ? -1 : 1); return a.response.success }; h.prototype.onMapKbdTab = function (a, e) {
                        var c = this.chart; a = a.response; var b = (e = e.shiftKey) &&
                            !this.focusedMapNavButtonIx || !e && this.focusedMapNavButtonIx; c.mapNavButtons[this.focusedMapNavButtonIx].setState(0); if (b) return c.mapZoom(), a[e ? "prev" : "next"]; this.focusedMapNavButtonIx += e ? -1 : 1; e = c.mapNavButtons[this.focusedMapNavButtonIx]; c.setFocusToElement(e.box, e.element); e.setState(2); return a.success
                    }; h.prototype.onMapKbdClick = function (a) { this.fakeClickEvent(this.chart.mapNavButtons[this.focusedMapNavButtonIx].element); return a.response.success }; h.prototype.onMapNavInit = function (a) {
                        var c = this.chart,
                        d = c.mapNavButtons[0], b = c.mapNavButtons[1]; d = 0 < a ? d : b; c.setFocusToElement(d.box, d.element); d.setState(2); this.focusedMapNavButtonIx = 0 < a ? 0 : 1
                    }; h.prototype.simpleButtonNavigation = function (a, e, d) {
                        var b = this.keyCodes, c = this, h = this.chart; return new r(h, {
                            keyCodeMap: [[[b.tab, b.up, b.down, b.left, b.right], function (a, c) { return this.response[a === b.tab && c.shiftKey || a === b.left || a === b.up ? "prev" : "next"] }], [[b.space, b.enter], function () { var a = d(this, h); return g(a, this.response.success) }]], validate: function () {
                                return h[a] &&
                                    h[a].box && c[e].buttonElement
                            }, init: function () { h.setFocusToElement(h[a].box, c[e].buttonElement) }
                        })
                    }; h.prototype.getKeyboardNavigation = function () { return [this.simpleButtonNavigation("resetZoomButton", "resetZoomProxyButton", function (a, e) { e.zoomOut() }), this.simpleButtonNavigation("drillUpButton", "drillUpProxyButton", function (a, e) { e.drillUp(); return a.response.prev }), this.getMapZoomNavigation()] }; return h
                }(a); (function (a) {
                    function g(a, e) {
                        var c = e || 3; e = this.getExtremes(); var b = (e.max - e.min) / c * a; c = e.max + b;
                        b = e.min + b; var f = c - b; 0 > a && b < e.dataMin ? (b = e.dataMin, c = b + f) : 0 < a && c > e.dataMax && (c = e.dataMax, b = c - f); this.setExtremes(b, c)
                    } a.composedClasses = []; a.compose = function (c) { -1 === a.composedClasses.indexOf(c) && (a.composedClasses.push(c), c.prototype.panStep = g) }
                })(a || (a = {})); return a
            }); x(a, "Accessibility/HighContrastMode.js", [a["Core/Globals.js"]], function (a) {
                var h = a.doc, k = a.isMS, r = a.win; return {
                    isHighContrastModeActive: function () {
                        var a = /(Edg)/.test(r.navigator.userAgent); if (r.matchMedia && a) return r.matchMedia("(-ms-high-contrast: active)").matches;
                        if (k && r.getComputedStyle) { a = h.createElement("div"); a.style.backgroundImage = "url(data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==)"; h.body.appendChild(a); var t = (a.currentStyle || r.getComputedStyle(a)).backgroundImage; h.body.removeChild(a); return "none" === t } return !1
                    }, setHighContrastTheme: function (a) {
                        a.highContrastModeActive = !0; var h = a.options.accessibility.highContrastTheme; a.update(h, !1); a.series.forEach(function (a) {
                            var k = h.plotOptions[a.type] || {}; a.update({
                                color: k.color || "windowText",
                                colors: [k.color || "windowText"], borderColor: k.borderColor || "window"
                            }); a.points.forEach(function (a) { a.options && a.options.color && a.update({ color: k.color || "windowText", borderColor: k.borderColor || "window" }, !1) })
                        }); a.redraw()
                    }
                }
            }); x(a, "Accessibility/HighContrastTheme.js", [], function () {
                return {
                    chart: { backgroundColor: "window" }, title: { style: { color: "windowText" } }, subtitle: { style: { color: "windowText" } }, colorAxis: { minColor: "windowText", maxColor: "windowText", stops: [] }, colors: ["windowText"], xAxis: {
                        gridLineColor: "windowText",
                        labels: { style: { color: "windowText" } }, lineColor: "windowText", minorGridLineColor: "windowText", tickColor: "windowText", title: { style: { color: "windowText" } }
                    }, yAxis: { gridLineColor: "windowText", labels: { style: { color: "windowText" } }, lineColor: "windowText", minorGridLineColor: "windowText", tickColor: "windowText", title: { style: { color: "windowText" } } }, tooltip: { backgroundColor: "window", borderColor: "windowText", style: { color: "windowText" } }, plotOptions: {
                        series: {
                            lineColor: "windowText", fillColor: "window", borderColor: "windowText",
                            edgeColor: "windowText", borderWidth: 1, dataLabels: { connectorColor: "windowText", color: "windowText", style: { color: "windowText", textOutline: "none" } }, marker: { lineColor: "windowText", fillColor: "windowText" }
                        }, pie: { color: "window", colors: ["window"], borderColor: "windowText", borderWidth: 1 }, boxplot: { fillColor: "window" }, candlestick: { lineColor: "windowText", fillColor: "window" }, errorbar: { fillColor: "window" }
                    }, legend: {
                        backgroundColor: "window", itemStyle: { color: "windowText" }, itemHoverStyle: { color: "windowText" }, itemHiddenStyle: { color: "#555" },
                        title: { style: { color: "windowText" } }
                    }, credits: { style: { color: "windowText" } }, labels: { style: { color: "windowText" } }, drilldown: { activeAxisLabelStyle: { color: "windowText" }, activeDataLabelStyle: { color: "windowText" } }, navigation: { buttonOptions: { symbolStroke: "windowText", theme: { fill: "window" } } }, rangeSelector: {
                        buttonTheme: { fill: "window", stroke: "windowText", style: { color: "windowText" }, states: { hover: { fill: "window", stroke: "windowText", style: { color: "windowText" } }, select: { fill: "#444", stroke: "windowText", style: { color: "windowText" } } } },
                        inputBoxBorderColor: "windowText", inputStyle: { backgroundColor: "window", color: "windowText" }, labelStyle: { color: "windowText" }
                    }, navigator: { handles: { backgroundColor: "window", borderColor: "windowText" }, outlineColor: "windowText", maskFill: "transparent", series: { color: "windowText", lineColor: "windowText" }, xAxis: { gridLineColor: "windowText" } }, scrollbar: {
                        barBackgroundColor: "#444", barBorderColor: "windowText", buttonArrowColor: "windowText", buttonBackgroundColor: "window", buttonBorderColor: "windowText", rifleColor: "windowText",
                        trackBackgroundColor: "window", trackBorderColor: "windowText"
                    }
                }
            }); x(a, "Accessibility/Options/Options.js", [], function () {
                return {
                    accessibility: {
                        enabled: !0, screenReaderSection: {
                            beforeChartFormat: "<{headingTagName}>{chartTitle}</{headingTagName}><div>{typeDescription}</div><div>{chartSubtitle}</div><div>{chartLongdesc}</div><div>{playAsSoundButton}</div><div>{viewTableButton}</div><div>{xAxisDescription}</div><div>{yAxisDescription}</div><div>{annotationsTitle}{annotationsList}</div>", afterChartFormat: "{endOfChartMarker}",
                            axisRangeDateFormat: "%Y-%m-%d %H:%M:%S"
                        }, series: { describeSingleSeries: !1, pointDescriptionEnabledThreshold: 200 }, point: { valueDescriptionFormat: "{index}. {xDescription}{separator}{value}." }, landmarkVerbosity: "all", linkedDescription: '*[data-highcharts-chart="{index}"] + .highcharts-description', keyboardNavigation: {
                            enabled: !0, focusBorder: { enabled: !0, hideBrowserFocusOutline: !0, style: { color: "#335cad", lineWidth: 2, borderRadius: 3 }, margin: 2 }, order: ["series", "zoom", "rangeSelector", "legend", "chartMenu"], wrapAround: !0,
                            seriesNavigation: { skipNullPoints: !0, pointNavigationEnabledThreshold: !1 }
                        }, announceNewData: { enabled: !1, minAnnounceInterval: 5E3, interruptUser: !1 }
                    }, legend: { accessibility: { enabled: !0, keyboardNavigation: { enabled: !0 } } }, exporting: { accessibility: { enabled: !0 } }
                }
            }); x(a, "Accessibility/Options/LangOptions.js", [], function () {
                return {
                    accessibility: {
                        defaultChartTitle: "Chart", chartContainerLabel: "{title}. Highcharts interactive chart.", svgContainerLabel: "Interactive chart", drillUpButton: "{buttonText}", credits: "Chart credits: {creditsStr}",
                        thousandsSep: ",", svgContainerTitle: "", graphicContainerLabel: "", screenReaderSection: { beforeRegionLabel: "Chart screen reader information, {chartTitle}.", afterRegionLabel: "", annotations: { heading: "Chart annotations summary", descriptionSinglePoint: "{annotationText}. Related to {annotationPoint}", descriptionMultiplePoints: "{annotationText}. Related to {annotationPoint}{ Also related to, #each(additionalAnnotationPoints)}", descriptionNoPoints: "{annotationText}" }, endOfChartMarker: "End of interactive chart." },
                        sonification: { playAsSoundButtonText: "Play as sound, {chartTitle}", playAsSoundClickAnnouncement: "Play" }, legend: { legendLabelNoTitle: "Toggle series visibility, {chartTitle}", legendLabel: "Chart legend: {legendTitle}", legendItem: "Show {itemName}" }, zoom: { mapZoomIn: "Zoom chart", mapZoomOut: "Zoom out chart", resetZoomButton: "Reset zoom" }, rangeSelector: { dropdownLabel: "{rangeTitle}", minInputLabel: "Select start date.", maxInputLabel: "Select end date.", clickButtonAnnouncement: "Viewing {axisRangeDescription}" }, table: {
                            viewAsDataTableButtonText: "View as data table, {chartTitle}",
                            tableSummary: "Table representation of chart."
                        }, announceNewData: { newDataAnnounce: "Updated data for chart {chartTitle}", newSeriesAnnounceSingle: "New data series: {seriesDesc}", newPointAnnounceSingle: "New data point: {pointDesc}", newSeriesAnnounceMultiple: "New data series in chart {chartTitle}: {seriesDesc}", newPointAnnounceMultiple: "New data point in chart {chartTitle}: {pointDesc}" }, seriesTypeDescriptions: {
                            boxplot: "Box plot charts are typically used to display groups of statistical data. Each data point in the chart can have up to 5 values: minimum, lower quartile, median, upper quartile, and maximum.",
                            arearange: "Arearange charts are line charts displaying a range between a lower and higher value for each point.", areasplinerange: "These charts are line charts displaying a range between a lower and higher value for each point.", bubble: "Bubble charts are scatter charts where each data point also has a size value.", columnrange: "Columnrange charts are column charts displaying a range between a lower and higher value for each point.", errorbar: "Errorbar series are used to display the variability of the data.",
                            funnel: "Funnel charts are used to display reduction of data in stages.", pyramid: "Pyramid charts consist of a single pyramid with item heights corresponding to each point value.", waterfall: "A waterfall chart is a column chart where each column contributes towards a total end value."
                        }, chartTypes: {
                            emptyChart: "Empty chart", mapTypeDescription: "Map of {mapTitle} with {numSeries} data series.", unknownMap: "Map of unspecified region with {numSeries} data series.", combinationChart: "Combination chart with {numSeries} data series.",
                            defaultSingle: "Chart with {numPoints} data {#plural(numPoints, points, point)}.", defaultMultiple: "Chart with {numSeries} data series.", splineSingle: "Line chart with {numPoints} data {#plural(numPoints, points, point)}.", splineMultiple: "Line chart with {numSeries} lines.", lineSingle: "Line chart with {numPoints} data {#plural(numPoints, points, point)}.", lineMultiple: "Line chart with {numSeries} lines.", columnSingle: "Bar chart with {numPoints} {#plural(numPoints, bars, bar)}.", columnMultiple: "Bar chart with {numSeries} data series.",
                            barSingle: "Bar chart with {numPoints} {#plural(numPoints, bars, bar)}.", barMultiple: "Bar chart with {numSeries} data series.", pieSingle: "Pie chart with {numPoints} {#plural(numPoints, slices, slice)}.", pieMultiple: "Pie chart with {numSeries} pies.", scatterSingle: "Scatter chart with {numPoints} {#plural(numPoints, points, point)}.", scatterMultiple: "Scatter chart with {numSeries} data series.", boxplotSingle: "Boxplot with {numPoints} {#plural(numPoints, boxes, box)}.", boxplotMultiple: "Boxplot with {numSeries} data series.",
                            bubbleSingle: "Bubble chart with {numPoints} {#plural(numPoints, bubbles, bubble)}.", bubbleMultiple: "Bubble chart with {numSeries} data series."
                        }, axis: {
                            xAxisDescriptionSingular: "The chart has 1 X axis displaying {names[0]}. {ranges[0]}", xAxisDescriptionPlural: "The chart has {numAxes} X axes displaying {#each(names, -1) }and {names[-1]}.", yAxisDescriptionSingular: "The chart has 1 Y axis displaying {names[0]}. {ranges[0]}", yAxisDescriptionPlural: "The chart has {numAxes} Y axes displaying {#each(names, -1) }and {names[-1]}.",
                            timeRangeDays: "Range: {range} days.", timeRangeHours: "Range: {range} hours.", timeRangeMinutes: "Range: {range} minutes.", timeRangeSeconds: "Range: {range} seconds.", rangeFromTo: "Range: {rangeFrom} to {rangeTo}.", rangeCategories: "Range: {numCategories} categories."
                        }, exporting: { chartMenuLabel: "Chart menu", menuButtonLabel: "View chart menu, {chartTitle}" }, series: {
                            summary: {
                                "default": "{name}, series {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", defaultCombination: "{name}, series {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.",
                                line: "{name}, line {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", lineCombination: "{name}, series {ix} of {numSeries}. Line with {numPoints} data {#plural(numPoints, points, point)}.", spline: "{name}, line {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", splineCombination: "{name}, series {ix} of {numSeries}. Line with {numPoints} data {#plural(numPoints, points, point)}.", column: "{name}, bar series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bars, bar)}.",
                                columnCombination: "{name}, series {ix} of {numSeries}. Bar series with {numPoints} {#plural(numPoints, bars, bar)}.", bar: "{name}, bar series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bars, bar)}.", barCombination: "{name}, series {ix} of {numSeries}. Bar series with {numPoints} {#plural(numPoints, bars, bar)}.", pie: "{name}, pie {ix} of {numSeries} with {numPoints} {#plural(numPoints, slices, slice)}.", pieCombination: "{name}, series {ix} of {numSeries}. Pie with {numPoints} {#plural(numPoints, slices, slice)}.",
                                scatter: "{name}, scatter plot {ix} of {numSeries} with {numPoints} {#plural(numPoints, points, point)}.", scatterCombination: "{name}, series {ix} of {numSeries}, scatter plot with {numPoints} {#plural(numPoints, points, point)}.", boxplot: "{name}, boxplot {ix} of {numSeries} with {numPoints} {#plural(numPoints, boxes, box)}.", boxplotCombination: "{name}, series {ix} of {numSeries}. Boxplot with {numPoints} {#plural(numPoints, boxes, box)}.", bubble: "{name}, bubble series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bubbles, bubble)}.",
                                bubbleCombination: "{name}, series {ix} of {numSeries}. Bubble series with {numPoints} {#plural(numPoints, bubbles, bubble)}.", map: "{name}, map {ix} of {numSeries} with {numPoints} {#plural(numPoints, areas, area)}.", mapCombination: "{name}, series {ix} of {numSeries}. Map with {numPoints} {#plural(numPoints, areas, area)}.", mapline: "{name}, line {ix} of {numSeries} with {numPoints} data {#plural(numPoints, points, point)}.", maplineCombination: "{name}, series {ix} of {numSeries}. Line with {numPoints} data {#plural(numPoints, points, point)}.",
                                mapbubble: "{name}, bubble series {ix} of {numSeries} with {numPoints} {#plural(numPoints, bubbles, bubble)}.", mapbubbleCombination: "{name}, series {ix} of {numSeries}. Bubble series with {numPoints} {#plural(numPoints, bubbles, bubble)}."
                            }, description: "{description}", xAxisDescription: "X axis, {name}", yAxisDescription: "Y axis, {name}", nullPointValue: "No value", pointAnnotationsDescription: "{Annotation: #each(annotations). }"
                        }
                    }
                }
            }); x(a, "Accessibility/Options/DeprecatedOptions.js", [a["Core/Utilities.js"]],
                function (a) {
                    function h(a, h, k) { for (var c, e = 0; e < h.length - 1; ++e)c = h[e], a = a[c] = n(a[c], {}); a[h[h.length - 1]] = k } function k(a, k, m, c) { function e(a, b) { return b.reduce(function (a, b) { return a[b] }, a) } var d = e(a.options, k), b = e(a.options, m); Object.keys(c).forEach(function (e) { var f, g = d[e]; "undefined" !== typeof g && (h(b, c[e], g), x(32, !1, a, (f = {}, f[k.join(".") + "." + e] = m.join(".") + "." + c[e].join("."), f))) }) } function r(a) {
                        var g = a.options.chart, h = a.options.accessibility || {};["description", "typeDescription"].forEach(function (c) {
                            var e;
                            g[c] && (h[c] = g[c], x(32, !1, a, (e = {}, e["chart." + c] = "use accessibility." + c, e)))
                        })
                    } function m(a) { a.axes.forEach(function (g) { (g = g.options) && g.description && (g.accessibility = g.accessibility || {}, g.accessibility.description = g.description, x(32, !1, a, { "axis.description": "use axis.accessibility.description" })) }) } function w(a) {
                        var g = {
                            description: ["accessibility", "description"], exposeElementToA11y: ["accessibility", "exposeAsGroupOnly"], pointDescriptionFormatter: ["accessibility", "point", "descriptionFormatter"], skipKeyboardNavigation: ["accessibility",
                                "keyboardNavigation", "enabled"], "accessibility.pointDescriptionFormatter": ["accessibility", "point", "descriptionFormatter"]
                        }; a.series.forEach(function (k) { Object.keys(g).forEach(function (c) { var e, d = k.options[c]; "accessibility.pointDescriptionFormatter" === c && (d = k.options.accessibility && k.options.accessibility.pointDescriptionFormatter); "undefined" !== typeof d && (h(k.options, g[c], "skipKeyboardNavigation" === c ? !d : d), x(32, !1, a, (e = {}, e["series." + c] = "series." + g[c].join("."), e))) }) })
                    } var x = a.error, n = a.pick; return function (a) {
                        r(a);
                        m(a); a.series && w(a); k(a, ["accessibility"], ["accessibility"], {
                            pointDateFormat: ["point", "dateFormat"], pointDateFormatter: ["point", "dateFormatter"], pointDescriptionFormatter: ["point", "descriptionFormatter"], pointDescriptionThreshold: ["series", "pointDescriptionEnabledThreshold"], pointNavigationThreshold: ["keyboardNavigation", "seriesNavigation", "pointNavigationEnabledThreshold"], pointValueDecimals: ["point", "valueDecimals"], pointValuePrefix: ["point", "valuePrefix"], pointValueSuffix: ["point", "valueSuffix"],
                            screenReaderSectionFormatter: ["screenReaderSection", "beforeChartFormatter"], describeSingleSeries: ["series", "describeSingleSeries"], seriesDescriptionFormatter: ["series", "descriptionFormatter"], onTableAnchorClick: ["screenReaderSection", "onViewDataTableClick"], axisRangeDateFormat: ["screenReaderSection", "axisRangeDateFormat"]
                        }); k(a, ["accessibility", "keyboardNavigation"], ["accessibility", "keyboardNavigation", "seriesNavigation"], { skipNullPoints: ["skipNullPoints"], mode: ["mode"] }); k(a, ["lang", "accessibility"],
                            ["lang", "accessibility"], {
                                legendItem: ["legend", "legendItem"], legendLabel: ["legend", "legendLabel"], mapZoomIn: ["zoom", "mapZoomIn"], mapZoomOut: ["zoom", "mapZoomOut"], resetZoomButton: ["zoom", "resetZoomButton"], screenReaderRegionLabel: ["screenReaderSection", "beforeRegionLabel"], rangeSelectorButton: ["rangeSelector", "buttonText"], rangeSelectorMaxInput: ["rangeSelector", "maxInputLabel"], rangeSelectorMinInput: ["rangeSelector", "minInputLabel"], svgContainerEnd: ["screenReaderSection", "endOfChartMarker"], viewAsDataTable: ["table",
                                    "viewAsDataTableButtonText"], tableSummary: ["table", "tableSummary"]
                        })
                    }
                }); x(a, "Accessibility/Accessibility.js", [a["Core/DefaultOptions.js"], a["Core/Globals.js"], a["Core/Utilities.js"], a["Accessibility/A11yI18n.js"], a["Accessibility/Components/ContainerComponent.js"], a["Accessibility/FocusBorder.js"], a["Accessibility/Components/InfoRegionsComponent.js"], a["Accessibility/KeyboardNavigation.js"], a["Accessibility/Components/LegendComponent.js"], a["Accessibility/Components/MenuComponent.js"], a["Accessibility/Components/SeriesComponent/NewDataAnnouncer.js"],
                a["Accessibility/ProxyProvider.js"], a["Accessibility/Components/RangeSelectorComponent.js"], a["Accessibility/Components/SeriesComponent/SeriesComponent.js"], a["Accessibility/Components/ZoomComponent.js"], a["Accessibility/HighContrastMode.js"], a["Accessibility/HighContrastTheme.js"], a["Accessibility/Options/Options.js"], a["Accessibility/Options/LangOptions.js"], a["Accessibility/Options/DeprecatedOptions.js"]], function (a, h, t, r, m, w, x, n, g, q, y, c, e, d, b, f, u, v, K, E) {
                    a = a.defaultOptions; var k = h.doc, z = t.addEvent,
                        B = t.extend, F = t.fireEvent, C = t.merge; h = function () {
                            function a(a) { this.proxyProvider = this.keyboardNavigation = this.components = this.chart = void 0; this.init(a) } a.prototype.init = function (a) { this.chart = a; k.addEventListener && a.renderer.isSVG ? (E(a), this.proxyProvider = new c(this.chart), this.initComponents(), this.keyboardNavigation = new n(a, this.components)) : (this.zombie = !0, this.components = {}, a.renderTo.setAttribute("aria-hidden", !0)) }; a.prototype.initComponents = function () {
                                var a = this.chart, c = this.proxyProvider,
                                f = a.options.accessibility; this.components = { container: new m, infoRegions: new x, legend: new g, chartMenu: new q, rangeSelector: new e, series: new d, zoom: new b }; f.customComponents && B(this.components, f.customComponents); var h = this.components; this.getComponentOrder().forEach(function (b) { h[b].initBase(a, c); h[b].init() })
                            }; a.prototype.getComponentOrder = function () {
                                if (!this.components) return []; if (!this.components.series) return Object.keys(this.components); var a = Object.keys(this.components).filter(function (a) {
                                    return "series" !==
                                        a
                                }); return ["series"].concat(a)
                            }; a.prototype.update = function () {
                                var a = this.components, b = this.chart, c = b.options.accessibility; F(b, "beforeA11yUpdate"); b.types = this.getChartTypes(); c = c.keyboardNavigation.order; this.proxyProvider.updateGroupOrder(c); this.getComponentOrder().forEach(function (c) { a[c].onChartUpdate(); F(b, "afterA11yComponentUpdate", { name: c, component: a[c] }) }); this.keyboardNavigation.update(c); !b.highContrastModeActive && f.isHighContrastModeActive() && f.setHighContrastTheme(b); F(b, "afterA11yUpdate",
                                    { accessibility: this })
                            }; a.prototype.destroy = function () { var a = this.chart || {}, b = this.components; Object.keys(b).forEach(function (a) { b[a].destroy(); b[a].destroyBase() }); this.proxyProvider && this.proxyProvider.destroy(); this.keyboardNavigation && this.keyboardNavigation.destroy(); a.renderTo && a.renderTo.setAttribute("aria-hidden", !0); a.focusElement && a.focusElement.removeFocusBorder() }; a.prototype.getChartTypes = function () { var a = {}; this.chart.series.forEach(function (b) { a[b.type] = 1 }); return Object.keys(a) }; return a
                        }();
                    (function (a) {
                        function c() { this.accessibility && this.accessibility.destroy() } function f() { this.a11yDirty && this.renderTo && (delete this.a11yDirty, this.updateA11yEnabled()); var a = this.accessibility; a && !a.zombie && (a.proxyProvider.updateProxyElementPositions(), a.getComponentOrder().forEach(function (b) { a.components[b].onChartRender() })) } function h(a) {
                            if (a = a.options.accessibility) a.customComponents && (this.options.accessibility.customComponents = a.customComponents, delete a.customComponents), C(!0, this.options.accessibility,
                                a), this.accessibility && this.accessibility.destroy && (this.accessibility.destroy(), delete this.accessibility); this.a11yDirty = !0
                        } function k() { var b = this.accessibility, c = this.options.accessibility; c && c.enabled ? b && !b.zombie ? b.update() : (this.accessibility = b = new a(this), !b.zombie) && b.update() : b ? (b.destroy && b.destroy(), delete this.accessibility) : this.renderTo.setAttribute("aria-hidden", !0) } function m() { this.series.chart.accessibility && (this.series.chart.a11yDirty = !0) } var t = []; a.i18nFormat = r.i18nFormat; a.compose =
                            function (a, l, p, u, v, x, A) {
                                n.compose(l); y.compose(v); g.compose(l, p); q.compose(l); d.compose(l, u, v); b.compose(a); r.compose(l); w.compose(l, x); A && e.compose(l, A); -1 === t.indexOf(l) && (t.push(l), l.prototype.updateA11yEnabled = k, z(l, "destroy", c), z(l, "render", f), z(l, "update", h), ["addSeries", "init"].forEach(function (a) { z(l, a, function () { this.a11yDirty = !0 }) }), ["afterDrilldown", "drillupall"].forEach(function (a) { z(l, a, function () { var a = this.accessibility; a && !a.zombie && a.update() }) })); -1 === t.indexOf(u) && (t.push(u), z(u,
                                    "update", m)); -1 === t.indexOf(v) && (t.push(v), ["update", "updatedData", "remove"].forEach(function (a) { z(v, a, function () { this.chart.accessibility && (this.chart.a11yDirty = !0) }) }))
                            }
                    })(h || (h = {})); C(!0, a, v, { accessibility: { highContrastTheme: u }, lang: K }); return h
                }); x(a, "masters/modules/accessibility.src.js", [a["Core/Globals.js"], a["Accessibility/Accessibility.js"], a["Accessibility/AccessibilityComponent.js"], a["Accessibility/Utils/ChartUtilities.js"], a["Accessibility/Utils/HTMLUtilities.js"], a["Accessibility/KeyboardNavigationHandler.js"],
                a["Accessibility/Components/SeriesComponent/SeriesDescriber.js"]], function (a, h, t, r, m, w, x) { a.i18nFormat = h.i18nFormat; a.A11yChartUtilities = r; a.A11yHTMLUtilities = m; a.AccessibilityComponent = t; a.KeyboardNavigationHandler = w; a.SeriesAccessibilityDescriber = x; h.compose(a.Axis, a.Chart, a.Legend, a.Point, a.Series, a.SVGElement, a.RangeSelector) })
});
//# sourceMappingURL=accessibility.js.map