//向Hashtable中添加键值对
function Hashtable() {
    this._hash = {};
    this._count = 0;
    this.add = function(key, value) {
        if (this._hash.hasOwnProperty(key)) return false;
        else { this._hash[key] = value; this._count++; return true; }
    }
    this.remove = function(key) { delete this._hash[key]; this._count--; }
    this.count = function() { return this._count; }
    this.items = function(key) { if (this.contains(key)) return this._hash[key]; }
    this.contains = function(key) { return this._hash.hasOwnProperty(key); }
    this.clear = function() { this._hash = {}; this._count = 0; }
}