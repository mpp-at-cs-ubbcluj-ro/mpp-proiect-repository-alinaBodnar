using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.repository
{
    interface Repository<Key,Element>
    {
        void save(Element el);
        void delete(Key key);
        void update(Element el);
        Element findOne(Key key);
        List<Element> findAll();
    }
}
